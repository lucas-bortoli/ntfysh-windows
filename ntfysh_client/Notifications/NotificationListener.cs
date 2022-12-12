using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace ntfysh_client.Notifications
{
    public class NotificationListener
    {
        public readonly Dictionary<string, SubscribedTopic?> SubscribedTopicsByUnique = new();

        public delegate void NotificationReceiveHandler(NotificationListener sender, NotificationReceiveEventArgs e);
        public event NotificationReceiveHandler? OnNotificationReceive;
        
        public delegate void ConnectionErrorHandler(NotificationListener sender, SubscribedTopic topic);
        public event ConnectionErrorHandler? OnConnectionMultiAttemptFailure;
        public event ConnectionErrorHandler? OnConnectionCredentialsFailure;

        public NotificationListener()
        {
            ServicePointManager.DefaultConnectionLimit = 100;
        }

        private async Task ListenToTopicWithHttpLongJsonAsync(HttpRequestMessage message, CancellationToken cancellationToken, SubscribedTopic topic)
        {
            int connectionAttempts = 0;
            
            while (!cancellationToken.IsCancellationRequested)
            {
                //See if we have exceeded maximum attempts
                if (connectionAttempts >= 10)
                {
                    //10 connection failures (1 initial + 9 reattempts)! Do not retry
                    OnConnectionMultiAttemptFailure?.Invoke(this, topic);
                    return;
                }
                
                try
                {
                    //Establish connection
                    using HttpClient client = new();
                    client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite); //This will not prevent us from failing to connect, luckily
                    
                    using HttpResponseMessage response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                    await using Stream body = await response.Content.ReadAsStreamAsync(cancellationToken);
                    
                    //Ensure successful connect
                    response.EnsureSuccessStatusCode();

                    //Reset connection attempts after a successful connect
                    connectionAttempts = 0;

                    //Begin listening
                    StringBuilder mainBuffer = new();

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //Read as much as possible
                        byte[] buffer = new byte[8192];
                        int readBytes = await body.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

                        //Append it to our main buffer
                        mainBuffer.Append(Encoding.UTF8.GetString(buffer, 0, readBytes));

                        List<string> lines = mainBuffer.ToString().Split('\n').ToList();

                        //If we have not yet received a full line, meaning theres only 1 part, go back to reading
                        if (lines.Count <= 1) continue;

                        //We now have at least 1 line! Count how many full lines. There will always be a partial line at the end, even if that partial line is empty

                        //Separate the partial line from the full lines
                        int partialLineIndex = lines.Count - 1;
                        string partialLine = lines[partialLineIndex];
                        lines.RemoveAt(partialLineIndex);

                        //Process the full lines
                        foreach (string line in lines) ProcessMessage(topic, line);

                        //Write back the partial line
                        mainBuffer.Clear();
                        mainBuffer.Append(partialLine);
                    }
                }
                catch (HttpRequestException hre)
                {
                    if (hre.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
                    {
                        //Our credentials either aren't present when they need to be or are invalid
                        
                        //Credential Failure! Do not retry
                        OnConnectionCredentialsFailure?.Invoke(this, topic);
                        return;
                    }

                    #if DEBUG
                        Debug.WriteLine(hre);
                    #endif
                    
                    //We will not hit the finally block which will increment the connection failure counter and attempt a reconnect if applicable
                }
                catch (Exception e)
                {
                    #if DEBUG
                        Debug.WriteLine(e);
                    #endif

                    //We will not hit the finally block which will increment the connection failure counter and attempt a reconnect if applicable
                }
                finally
                {
                    //We land here if we fail to connect or our connection gets closed (and if we are canceeling, but that gets ignored)
                    
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        //Not cancelling, legitimate connection failure or termination

                        if (connectionAttempts != 0)
                        {
                            //On our first reconnect attempt, try instantly. On consecutive, wait 3 seconds before each attempt
                            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
                        }

                        //Increment attempts
                        connectionAttempts++;
                        
                        //Proceed to reattempt
                    }
                }
            }
        }
        
        private async Task ListenToTopicWithWebsocketAsync(Uri uri, string? credentials, CancellationToken cancellationToken, SubscribedTopic topic)
        {
            int connectionAttempts = 0;
            
            while (!cancellationToken.IsCancellationRequested)
            {
                //See if we have exceeded maximum attempts
                if (connectionAttempts >= 10)
                {
                    //10 connection failures (1 initial + 9 reattempts)! Do not retry
                    OnConnectionMultiAttemptFailure?.Invoke(this, topic);
                    return;
                }

                try
                {
                    //Establish connection
                    using ClientWebSocket socket = new();

                    if (!string.IsNullOrWhiteSpace(credentials)) socket.Options.SetRequestHeader("Authorization", "Basic " + credentials);

                    await socket.ConnectAsync(uri, cancellationToken);
                    
                    //Reset connection attempts after a successful connect
                    connectionAttempts = 0;
                    
                    //Begin listening
                    StringBuilder mainBuffer = new();

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //Read as much as possible
                        byte[] buffer = new byte[8192];
                        WebSocketReceiveResult? result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                        //Append it to our main buffer
                        mainBuffer.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));

                        List<string> lines = mainBuffer.ToString().Split('\n').ToList();
                        //If we have not yet received a full line, meaning theres only 1 part, go back to reading
                        if (lines.Count <= 1) continue;

                        //We now have at least 1 line! Count how many full lines. There will always be a partial line at the end, even if that partial line is empty
                        //Separate the partial line from the full lines
                        int partialLineIndex = lines.Count - 1;
                        string partialLine = lines[partialLineIndex];
                        lines.RemoveAt(partialLineIndex);

                        //Process the full lines
                        foreach (string line in lines) ProcessMessage(topic, line);

                        //Write back the partial line
                        mainBuffer.Clear();
                        mainBuffer.Append(partialLine);
                    }
                }
                catch (WebSocketException wse)
                {
                    if (wse.WebSocketErrorCode is WebSocketError.NotAWebSocket)
                    {
                        //We haven't achieved a connection with a websocket. TODO Seems ntfy doesn't report unauthorised properly, and responds 200
                        
                        //Credential Failure! Do not retry
                        OnConnectionCredentialsFailure?.Invoke(this, topic);
                        return;
                    }
                    
                    #if DEBUG
                        Debug.WriteLine(wse);
                    #endif
                    
                    //We will not hit the finally block which will increment the connection failure counter and attempt a reconnect if applicable
                }
                catch (Exception e)
                {
                    #if DEBUG
                        Debug.WriteLine(e);
                    #endif
                    
                    //We will not hit the finally block which will increment the connection failure counter and attempt a reconnect if applicable
                }
                finally
                {
                    //We land here if we fail to connect or our connection gets closed (and if we are canceeling, but that gets ignored)
                    
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        //Not cancelling, legitimate connection failure or termination

                        if (connectionAttempts != 0)
                        {
                            //On our first reconnect attempt, try instantly. On consecutive, wait 3 seconds before each attempt
                            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
                        }

                        //Increment attempts
                        connectionAttempts++;
                        
                        //Proceed to reattempt
                    }
                }
            }
        }

        private void ProcessMessage(SubscribedTopic topic, string message)
        {
            #if DEBUG
                Debug.WriteLine(message);
            #endif

            NtfyEvent? evt = JsonConvert.DeserializeObject<NtfyEvent>(message);
                    
            //If we hit this, ntfy sent us an invalid message
            if (evt is null) return;

            if (evt.Event == "message")
            {
                OnNotificationReceive?.Invoke(this, new NotificationReceiveEventArgs(topic, evt.Title ?? "", evt.Message, evt.Priority ?? NotificationPriority.Default));
            }
        }

        public void SubscribeToTopicUsingLongHttpJson(string unique, string topicId, string serverUrl, string? username, string? password)
        {
            if (SubscribedTopicsByUnique.ContainsKey(unique)) throw new InvalidOperationException("A topic with this unique already exists");
            
            if (string.IsNullOrWhiteSpace(username)) username = null;
            if (string.IsNullOrWhiteSpace(password)) password = null;
            
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"{serverUrl}/{HttpUtility.UrlEncode(topicId)}/json");

            if (username is not null && password is not null)
            {
                byte[] boundCredentialsBytes = Encoding.UTF8.GetBytes($"{username}:{password}");

                message.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(boundCredentialsBytes));
            }

            SubscribedTopic newTopic = new(topicId, serverUrl, username, password);

            CancellationTokenSource listenCanceller = new();
            Task listenTask = ListenToTopicWithHttpLongJsonAsync(message, listenCanceller.Token, newTopic);
            
            newTopic.SetAssociatedRunner(listenTask, listenCanceller);

            SubscribedTopicsByUnique.Add(unique, newTopic);
        }
        
        public void SubscribeToTopicUsingWebsocket(string unique, string topicId, string serverUrl, string? username, string? password)
        {
            if (SubscribedTopicsByUnique.ContainsKey(unique)) throw new InvalidOperationException("A topic with this unique already exists");
            
            if (string.IsNullOrWhiteSpace(username)) username = null;
            if (string.IsNullOrWhiteSpace(password)) password = null;
            
            SubscribedTopic newTopic = new(topicId, serverUrl, username, password);

            string? credentials = null;
            
            if (username is not null && password is not null)
            {
                byte[] boundCredentialsBytes = Encoding.UTF8.GetBytes($"{username}:{password}");

                credentials = Convert.ToBase64String(boundCredentialsBytes);
            }
            
            CancellationTokenSource listenCanceller = new();
            Task listenTask = ListenToTopicWithWebsocketAsync(new Uri($"{serverUrl}/{HttpUtility.UrlEncode(topicId)}/ws"), credentials, listenCanceller.Token, newTopic);
            
            newTopic.SetAssociatedRunner(listenTask, listenCanceller);
            
            SubscribedTopicsByUnique.Add(unique, newTopic);
        }

        public async Task UnsubscribeFromTopicAsync(string topicUniqueString)
        {
            #if DEBUG
                Debug.WriteLine($"Removing topic {topicUniqueString}");
            #endif
            
            // ReSharper disable once InlineOutVariableDeclaration - Needed to avoid nullable warning
            SubscribedTopic topic;

            //Topic isn't even subscribed, ignore
            if (!SubscribedTopicsByUnique.TryGetValue(topicUniqueString, out topic!)) return;
            
            //Cancel and dispose the task runner
            topic.RunnerCanceller?.Cancel();

            //Wait for the task runner to shut down
            try
            {
                if (topic.Runner is not null) await topic.Runner;
            }
            catch (Exception)
            {
                // ignored
            }

            //Dispose task
            topic.Runner?.Dispose();

            //Remove the old topic
            SubscribedTopicsByUnique.Remove(topicUniqueString);
        }
    }
}
