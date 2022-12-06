using System;
using System.Windows.Forms;

namespace ntfysh_client
{
    public partial class SubscribeDialog : Form
    {
        private readonly ListBox _notificationTopics;

        public SubscribeDialog(ListBox notificationTopics)
        {
            _notificationTopics = notificationTopics;
            InitializeComponent();
        }

        public string getTopicId()
        {
            return topicId.Text;
        }
        
        public string getServerUrl()
        {
            return serverUrl.Text;
        }
        
        public string getUsername()
        {
            return username.Text;
        }
        
        public string getPassword()
        {
            return password.Text;
        }

        public string getUniqueString()
        {
            return $"{topicId.Text}@{serverUrl.Text}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (topicId.Text.Length < 1)
            {
                MessageBox.Show("You must specify a topic name.", "Topic name not specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                topicId.Focus();
                return;
            }

            if (serverUrl.Text.Length < 1)
            {
                MessageBox.Show("You must specify a server URL. The default is https://ntfy.sh", "Server URL not specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                serverUrl.Focus();
                return;
            }

            if (username.Text.Length > 0 && password.Text.Length < 1)
            {
                MessageBox.Show("You must specify a password alongside the username", "Password not specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                password.Focus();
                return;
            }

            if (password.Text.Length > 0 && username.Text.Length < 1)
            {
                MessageBox.Show("You must specify a username alongside the password", "Username not specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                username.Focus();
                return;
            }

            if (_notificationTopics.Items.Contains(getUniqueString()))
            {
                MessageBox.Show($"The specified topic '{topicId.Text}' on the server '{serverUrl.Text}' is already subscribed", "Topic already subscribed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                username.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void topicId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        
        private void serverUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
