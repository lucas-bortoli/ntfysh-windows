namespace ntfysh_client
{
    public class SettingsModel
    {
        public uint Revision { get; set; }
        public decimal Timeout { get; set; }
        public decimal ReconnectAttempts { get; set; }
        public decimal ReconnectAttemptDelay { get; set; }
    }
}