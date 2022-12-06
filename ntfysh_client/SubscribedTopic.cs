﻿using System.IO;
using Newtonsoft.Json;

namespace ntfysh_client
{
    public class SubscribedTopic
    {
        public SubscribedTopic(string topicId, string serverUrl, string username, string password, StreamReader stream)
        {
            TopicId = topicId;
            ServerUrl = serverUrl;
            Username = username;
            Password = password;
            Stream = stream;
        }

        public string TopicId { get; }
        public string ServerUrl { get; }
        public string Username { get; }
        public string Password { get; }

        [JsonIgnore]
        public StreamReader Stream { get; }
    }
}