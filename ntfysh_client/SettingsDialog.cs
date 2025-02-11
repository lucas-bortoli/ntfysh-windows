using System;
using System.Windows.Forms;
using static ntfysh_client.SettingsModel;

namespace ntfysh_client
{
    public partial class SettingsDialog : Form
    {
        public NotificationsType NotificationsMethod { get; set; }

        public decimal Timeout
        {
            get => timeout.Value;
            set => timeout.Value = value;
        }

        public decimal ReconnectAttempts
        {
            get => reconnectAttempts.Value;
            set => reconnectAttempts.Value = value;
        }

        public decimal ReconnectAttemptDelay
        {
            get => reconnectAttemptDelay.Value;
            set => reconnectAttemptDelay.Value = value;
        }

        #region: Native vs custom notifications options. Because these are in a group box, these are mutualy exclusive.
        public bool UseNativeWindowsNotifications
        {
            get => useNativeWindowsNotifications.Checked;
            set
            {
                useNativeWindowsNotifications.Checked = value;
                groupCustomNotificationSettings.Enabled = !value;
                NotificationsMethod = (value) ? NotificationsType.NativeWindows : NotificationsType.CustomTray;
            }
        }

        public bool UseCustomTrayNotifications
        {
            get => useCustomTrayNotifications.Checked;
            set {
                useCustomTrayNotifications.Checked = value;
                groupCustomNotificationSettings.Enabled = value;
                NotificationsMethod = (value) ? NotificationsType.NativeWindows : NotificationsType.CustomTray;
            }
        }
        #endregion

        #region: Custom tray notification options
        public bool CustomTrayNotificationsShowTimeoutBar
        {
            get => customNotificationsShowTimeoutBar.Checked;
            set => customNotificationsShowTimeoutBar.Checked = value;
        }

        public bool CustomTrayNotificationsShowInDarkMode
        {
            get => customNotificationsShowInDarkMode.Checked;
            set => customNotificationsShowInDarkMode.Checked = value;
        }

        public bool CustomTrayNotificationsPlayDefaultWindowsSound
        {
            get => customNotificationsPlayWindowsNotificationAudio.Checked;
            set => customNotificationsPlayWindowsNotificationAudio.Checked = value;
        }
        #endregion

        public SettingsDialog()
        {
            InitializeComponent();
            SetNotificationsUiElements();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SetNotificationsUiElements()
        {
            groupCustomNotificationSettings.Enabled = useCustomTrayNotifications.Checked;
            timeoutLabel.Text = useCustomTrayNotifications.Checked ? _customNotificationsTimeout : _windowsNotificationsTimeout;
        }

        private void UseCustomTrayNotifications_CheckedChanged(object sender, EventArgs e)
        {
            SetNotificationsUiElements();
        }

        private const string _windowsNotificationsTimeout = "Notification Toast Timeout (seconds, may be ignored by OS based on accessibility settings):";
        private const string _customNotificationsTimeout = "Notification Toast Timeout (seconds, use 0 to require closing notification):";
    }
}
