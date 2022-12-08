using System;

namespace ntfysh_client.Notifications
{
    public class NotificationReceiveEventArgs : EventArgs
    {
        public string Title { get; }
        public string Message { get; }

        public NotificationReceiveEventArgs(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}