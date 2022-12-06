using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ntfysh_client
{
    class NotificationListener : IDisposable
    {
        private HttpClient httpClient;
        
        private bool disposedValue;

        public readonly Dictionary<string, SubscribedTopic> SubscribedTopicsByUnique = new Dictionary<string, SubscribedTopic>();

        public delegate void NotificationReceiveHandler(object sender, NotificationReceiveEventArgs e);
        public event NotificationReceiveHandler OnNotificationReceive;

        public NotificationListener()
        {
            httpClient = new HttpClient();

            httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            ServicePointManager.DefaultConnectionLimit = 100;
        }

        public async Task SubscribeToTopic(string unique, string topicId, string serverUrl, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) username = null;
            if (string.IsNullOrWhiteSpace(password)) password = null;
            
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, $"{serverUrl}/{HttpUtility.UrlEncode(topicId)}/json");

            if (username != null && password != null)
            {
                byte[] boundCredentialsBytes = Encoding.UTF8.GetBytes($"{username}:{password}");

                msg.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(boundCredentialsBytes));
            }

            using (HttpResponseMessage response = await httpClient.SendAsync(msg, HttpCompletionOption.ResponseHeadersRead))
            {
                using (Stream body = await response.Content.ReadAsStreamAsync())
                {
                    using (StreamReader reader = new StreamReader(body))
                    {
                        SubscribedTopicsByUnique.Add(unique, new SubscribedTopic(topicId, serverUrl, username, password, reader));

                        try
                        {
                            // The loop will be broken when this stream is closed
                            while (true)
                            {
                                var line = await reader.ReadLineAsync();

                                Debug.WriteLine(line);

                                NtfyEventObject nev = JsonConvert.DeserializeObject<NtfyEventObject>(line);

                                if (nev.Event == "message")
                                {
                                    if (OnNotificationReceive != null)
                                    {
                                        var evArgs = new NotificationReceiveEventArgs(nev.Title, nev.Message);
                                        OnNotificationReceive(this, evArgs);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);

                            // If the topic is still registered, then that stream wasn't mean to be closed (maybe network failure?)
                            // Restart it
                            if (SubscribedTopicsByUnique.ContainsKey(unique))
                            {
                                SubscribeToTopic(unique, topicId, serverUrl, username, password);
                            }
                        }
                    }
                }
            }
        }

        public void RemoveTopicByUniqueString(string topicUniqueString)
        {
            Debug.WriteLine($"Removing topic {topicUniqueString}");

            if (SubscribedTopicsByUnique.ContainsKey(topicUniqueString))
            {
                // Not moronic to store it in a variable; this solves a race condition in SubscribeToTopic
                SubscribedTopic topic = SubscribedTopicsByUnique[topicUniqueString];
                topic.Stream.Close();
                
                SubscribedTopicsByUnique.Remove(topicUniqueString);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~NotificationListener()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class NotificationReceiveEventArgs : EventArgs
    {
        public string Title { get; private set; }
        public string Message { get; private set; }

        public NotificationReceiveEventArgs(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }

    public class NtfyEventObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("time")]
        public Int64 Time { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
