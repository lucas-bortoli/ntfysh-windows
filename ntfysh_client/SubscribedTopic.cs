using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ntfysh_client
{
    public class SubscribedTopic
    {
        public SubscribedTopic(string topicId, string serverUrl, string? username, string? password, Task runner, CancellationTokenSource runnerCanceller)
        {
            TopicId = topicId;
            ServerUrl = serverUrl;
            Username = username;
            Password = password;
            Runner = runner;
            RunnerCanceller = runnerCanceller;
        }

        public string TopicId { get; }
        public string ServerUrl { get; }
        public string? Username { get; }
        public string? Password { get; }

        [JsonIgnore]
        public Task Runner { get; }
        
        [JsonIgnore]
        public CancellationTokenSource RunnerCanceller { get; }
    }
}