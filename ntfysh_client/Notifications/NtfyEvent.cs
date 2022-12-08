using Newtonsoft.Json;

namespace ntfysh_client.Notifications
{
    public class NtfyEvent
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("time")]
        public long Time { get; set; }
        
        [JsonProperty("event")]
        public string Event { get; set; } = null!;

        [JsonProperty("topic")]
        public string Topic { get; set; } = null!;

        [JsonProperty("message")]
        public string Message { get; set; } = null!;

        [JsonProperty("title")]
        public string Title { get; set; } = null!;
    }
}