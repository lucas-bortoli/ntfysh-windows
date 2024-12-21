namespace ntfysh_client
{
    public class SettingsModel
    {
        public uint Revision { get; set; }
        public decimal Timeout { get; set; }
        public decimal ReconnectAttempts { get; set; }
        public decimal ReconnectAttemptDelay { get; set; }
        public bool UseNativeWindowsNotifications { get; set; }
        public bool UseCustomTrayNotifications { get; set; }
        public bool CustomTrayNotificationsShowTimeoutBar { get; set; }
        public bool CustomTrayNotificationsShowInDarkMode { get; set; }
    }
}