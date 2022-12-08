using System;
using System.Windows.Forms;

namespace ntfysh_client
{
    public partial class SubscribeDialog : Form
    {
        private readonly ListBox _notificationTopics;
        
        public string TopicId => topicId.Text;
        
        public string ServerUrl => serverUrl.Text;
        
        public string Username => username.Text;
        
        public string Password => password.Text;

        public string Unique => $"{topicId.Text}@{serverUrl.Text}";

        public bool UseWebsockets
        {
            get
            {
                switch (connectionType.Text)
                {
                    case "Websockets (Recommended)":
                        return true;

                    case "Long HTTP JSON (Robust)":
                        return false;

                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public SubscribeDialog(ListBox notificationTopics)
        {
            _notificationTopics = notificationTopics;
            InitializeComponent();
        }

        private void SubscribeDialog_Load(object sender, EventArgs e)
        {
            connectionType.SelectedIndex = 0;
        }

        private bool ReparseAddress()
        {
            //Separate schema and address
            string[] parts = serverUrl.Text.Split("://", 2);

            //Validate the basic formatting is correct
            if (parts.Length != 2) return false;

            //Take the schema aside for parsing
            string schema = parts[0].ToLower();

            //Ensure the schema is actually valid
            switch (schema)
            {
                case "http":
                case "https":
                case "ws":
                case "wss":
                    break;

                default:
                    return false;
            }

            //Correct the schema based on connection type if required
            if (UseWebsockets)
            {
                switch (schema)
                {
                    case "http":
                        schema = "ws";
                        break;

                    case "https":
                        schema = "wss";
                        break;

                    case "ws":
                    case "wss":
                        break;
                }
            }
            else
            {
                switch (schema)
                {
                    case "ws":
                        schema = "http";
                        break;

                    case "wss":
                        schema = "https";
                        break;

                    case "http":
                    case "https":
                        break;
                }
            }

            //Reconstruct the address
            string finalAddress = schema + "://" + parts[1];

            //Validate the address
            if (!Uri.IsWellFormedUriString(finalAddress, UriKind.Absolute)) return false;

            //Set the final address and OK it
            serverUrl.Text = finalAddress;

            return true;
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
                MessageBox.Show("You must specify a server URL. The default is wss://ntfy.sh", "Server URL not specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (_notificationTopics.Items.Contains(Unique))
            {
                MessageBox.Show($"The specified topic '{topicId.Text}' on the server '{serverUrl.Text}' is already subscribed", "Topic already subscribed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                username.Focus();
                return;
            }

            try
            {
                if (!ReparseAddress())
                {
                    MessageBox.Show($"The specified server URL is invalid. Accepted schemas are: http:// https:// ws:// wss://", "Invalid Server URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                    connectionType.Focus();
                    return;
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show($"The selected Connection Type '{connectionType.Text}' is invalid.", "Invalid Connection Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                connectionType.Focus();
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

        private void connectionType_TextChanged(object sender, EventArgs e)
        {
            ReparseAddress();
        }
    }
}
