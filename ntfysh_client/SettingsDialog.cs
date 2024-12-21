using System;
using System.Windows.Forms;

namespace ntfysh_client
{
    public partial class SettingsDialog : Form
    {
        public decimal Timeout
        {
            get => timeout.Value;
            set => timeout.Value = Math.Max(value, timeout.Minimum); // Ensure value is within bounds despite our changing minimum
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
            }
        }

        public bool UseCustomTrayNotifications
        {
            get => useCustomTrayNotifications.Checked;
            set {
                useCustomTrayNotifications.Checked = value;
                groupCustomNotificationSettings.Enabled = value;
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
        #endregion

        public SettingsDialog()
        {
            InitializeComponent();
            SetNotificationsUIElements();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SetNotificationsUIElements()
        {
            groupCustomNotificationSettings.Enabled = useCustomTrayNotifications.Checked;
            timeoutLabel.Text = useCustomTrayNotifications.Checked ? _customNotificationsTimeout : _windowsNotificationsTimeout;
            timeout.Minimum = useCustomTrayNotifications.Checked ? -1 : 0;
        }

        private void UseCustomTrayNotifications_CheckedChanged(object sender, EventArgs e)
        {
            SetNotificationsUIElements();
        }

        private const string _windowsNotificationsTimeout = "Notification Toast Timeout (seconds, may be ignored by OS based on accessibility settings):";
        private const string _customNotificationsTimeout = "Notification Toast Timeout (seconds, use -1 to require closing notification):";
    }
}
