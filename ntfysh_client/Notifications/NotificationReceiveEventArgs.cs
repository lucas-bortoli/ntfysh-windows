using System;

namespace ntfysh_client.Notifications
{
    public class NotificationReceiveEventArgs : EventArgs
    {
        public SubscribedTopic Sender { get; }
        public string Title { get; }
        public string Message { get; }
        public NotificationPriority Priority { get; set; }

        public NotificationReceiveEventArgs(SubscribedTopic sender, string title, string message, NotificationPriority priority)
        {
            Sender = sender;
            Title = title;
            Message = message;
            Priority = priority;
        }
    }
}