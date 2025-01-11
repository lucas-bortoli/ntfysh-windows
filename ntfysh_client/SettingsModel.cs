namespace ntfysh_client
{
    public class SettingsModel
    {
        public enum NotificationsType
        {
            NativeWindows,
            CustomTray
        }

        public uint Revision { get; set; }
        public decimal Timeout { get; set; }
        public decimal ReconnectAttempts { get; set; }
        public decimal ReconnectAttemptDelay { get; set; }
        public NotificationsType NotificationsMethod { get; set; }
        public bool CustomTrayNotificationsShowTimeoutBar { get; set; }
        public bool CustomTrayNotificationsShowInDarkMode { get; set; }
        public bool CustomTrayNotificationsPlayDefaultWindowsSound { get; set; }
    }
}