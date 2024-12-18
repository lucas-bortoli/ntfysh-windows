using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace ntfysh_client.Notifications
{
    internal class PublishEvent
    {
        [JsonProperty("topic")]
        public string Topic { get; set; } = null!;

        [JsonProperty("message")]
        public string? Message { get; set; } = null;

        [JsonProperty("title")]
        public string? Title { get; set; } = null;

        [JsonProperty("tags")]
        public string[] Tags { get; set; } = null!;

        [JsonProperty("priority")]
        public NotificationPriority? Priority { get; set; } = null;

        [JsonProperty("actions")]
        public JsonArray? Actions { get; set; } = null;

        [JsonProperty("click")]
        public string? Click { get; set; } = null;

        [JsonProperty("attach")]
        public string? Attach { get; set; } = null;

        [JsonProperty("markdown")]
        public bool? Markdown { get; set; } = null;

        [JsonProperty("icon")]
        public string? Icon { get; set; } = null;

        [JsonProperty("filename")]
        public string? Filename { get; set; } = null;

        [JsonProperty("delay")]
        public string? Delay { get; set; } = null;

        [JsonProperty("email")]
        public string? Email { get; set; } = null;

        [JsonProperty("call")]
        public string? Call { get; set; } = null;
    }
}
