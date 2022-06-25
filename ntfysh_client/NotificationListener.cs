using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ntfysh_client
{
    class NotificationListener : IDisposable
    {
        private HttpClient httpClient;
        
        private bool disposedValue;

        public Dictionary<string, StreamReader> subscribedTopics;

        public delegate void NotificationReceiveHandler(object sender, NotificationReceiveEventArgs e);
        public event NotificationReceiveHandler OnNotificationReceive;

        public NotificationListener()
        {
            httpClient = new HttpClient();
            subscribedTopics = new Dictionary<string, StreamReader>();

            ServicePointManager.DefaultConnectionLimit = 100;
        }

        public async Task SubscribeToTopic(string topicId)
        {
            var stream = await httpClient.GetStreamAsync($"https://ntfy.sh/{HttpUtility.UrlEncode(topicId)}/json");

            using (StreamReader reader = new StreamReader(stream))
            {
                subscribedTopics.Add(topicId, reader);

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
                } catch(Exception ex)
                {
                    Debug.WriteLine(ex);

                    // If the topic is still registered, then that stream wasn't mean to be closed (maybe network failure?)
                    // Restart it
                    if (subscribedTopics.ContainsKey(topicId))
                    {
                        SubscribeToTopic(topicId);
                    }
                }
            }
        }

        public void RemoveTopic(string topicId)
        {
            Debug.WriteLine($"Removing topic {topicId}");

            if (subscribedTopics.ContainsKey(topicId))
            {
                // Not moronic to store it in a variable; this solves a race condition in SubscribeToTopic
                var topic = subscribedTopics[topicId];
                subscribedTopics.Remove(topicId);
                topic.Close();
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
