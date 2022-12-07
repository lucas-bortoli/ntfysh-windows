using Newtonsoft.Json;
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

namespace ntfysh_client
{
    public class NotificationListener : IDisposable
    {
        private readonly HttpClient _httpClient = new();
        private bool _isDisposed;

        public readonly Dictionary<string, SubscribedTopic?> SubscribedTopicsByUnique = new();

        public delegate void NotificationReceiveHandler(object sender, NotificationReceiveEventArgs e);
        public event NotificationReceiveHandler? OnNotificationReceive;

        public NotificationListener()
        {
            _httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            ServicePointManager.DefaultConnectionLimit = 100;
        }

        private async Task ListenToTopicWithHttpLongJsonAsync(HttpRequestMessage message, CancellationToken cancellationToken)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(NotificationListener));
            
            while (!cancellationToken.IsCancellationRequested)
            {
                using HttpResponseMessage response = await _httpClient.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                await using Stream body = await response.Content.ReadAsStreamAsync(cancellationToken);
            
                try
                {
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
                        foreach (string line in lines) ProcessMessage(line);
                        
                        //Write back the partial line
                        mainBuffer.Clear();
                        mainBuffer.Append(partialLine);
                    }
                }
                catch (Exception ex)
                {
                    #if DEBUG
                        Debug.WriteLine(ex);
                    #endif
                    
                    //Fall back to the outer loop to restart the listen, or cancel if requested
                }
            }
        }
        
        private async Task ListenToTopicWithWebsocketAsync(Uri uri, NetworkCredential credentials, CancellationToken cancellationToken)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(NotificationListener));
            
            while (!cancellationToken.IsCancellationRequested)
            {
                using ClientWebSocket socket = new();
                socket.Options.Credentials = credentials;
                
                try
                {
                    StringBuilder mainBuffer = new();
                    
                    await socket.ConnectAsync(uri, cancellationToken);
                    
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
                        foreach (string line in lines) ProcessMessage(line);
                        
                        //Write back the partial line
                        mainBuffer.Clear();
                        mainBuffer.Append(partialLine);
                    }
                }
                catch (Exception ex)
                {
                    #if DEBUG
                        Debug.WriteLine(ex);
                    #endif
                    
                    //Fall back to the outer loop to restart the listen, or cancel if requested
                }
            }
        }

        private void ProcessMessage(string message)
        {
            #if DEBUG
                Debug.WriteLine(message);
            #endif

            NtfyEvent? evt = JsonConvert.DeserializeObject<NtfyEvent>(message);
                    
            //If we hit this, ntfy sent us an invalid message
            if (evt is null) return;

            if (evt.Event == "message")
            {
                OnNotificationReceive?.Invoke(this, new NotificationReceiveEventArgs(evt.Title, evt.Message));
            }
        }

        public void SubscribeToTopicUsingLongHttpJson(string unique, string topicId, string serverUrl, string? username, string? password)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(NotificationListener));
            
            if (SubscribedTopicsByUnique.ContainsKey(unique)) throw new InvalidOperationException("A topic with this unique already exists");
            
            if (string.IsNullOrWhiteSpace(username)) username = null;
            if (string.IsNullOrWhiteSpace(password)) password = null;
            
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"{serverUrl}/{HttpUtility.UrlEncode(topicId)}/json");

            if (username != null && password != null)
            {
                byte[] boundCredentialsBytes = Encoding.UTF8.GetBytes($"{username}:{password}");

                message.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(boundCredentialsBytes));
            }

            CancellationTokenSource listenCanceller = new();
            Task listenTask = ListenToTopicWithHttpLongJsonAsync(message, listenCanceller.Token);

            SubscribedTopicsByUnique.Add(unique, new SubscribedTopic(topicId, serverUrl, username, password, listenTask, listenCanceller));
        }
        
        public void SubscribeToTopicUsingWebsocket(string unique, string topicId, string serverUrl, string? username, string? password)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(NotificationListener));
            
            if (SubscribedTopicsByUnique.ContainsKey(unique)) throw new InvalidOperationException("A topic with this unique already exists");
            
            if (string.IsNullOrWhiteSpace(username)) username = null;
            if (string.IsNullOrWhiteSpace(password)) password = null;
            
            Uri targetUri = new($"{serverUrl}/{HttpUtility.UrlEncode(topicId)}/ws");
            CancellationTokenSource listenCanceller = new();
            Task listenTask = ListenToTopicWithWebsocketAsync(targetUri, new NetworkCredential(username, password), listenCanceller.Token);
            SubscribedTopicsByUnique.Add(unique, new SubscribedTopic(topicId, serverUrl, username, password, listenTask, listenCanceller));
        }

        public async Task UnsubscribeFromTopicAsync(string topicUniqueString)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(NotificationListener));
                
            #if DEBUG
                Debug.WriteLine($"Removing topic {topicUniqueString}");
            #endif
            
            // ReSharper disable once InlineOutVariableDeclaration - Needed to avoid nullable warning
            SubscribedTopic topic;

            //Topic isn't even subscribed, ignore
            if (!SubscribedTopicsByUnique.TryGetValue(topicUniqueString, out topic!)) return;
            
            //Cancel and dispose the task runner
            topic.RunnerCanceller.Cancel();

            //Wait for the task runner to shut down
            try
            {
                await topic.Runner;
            }
            catch (Exception)
            {
                // ignored
            }

            //Dispose task
            topic.Runner.Dispose();

            //Remove the old topic
            SubscribedTopicsByUnique.Remove(topicUniqueString);
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            
            _httpClient.Dispose();
            
            _isDisposed = true;
        }
    }
}
