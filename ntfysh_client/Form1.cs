using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ntfysh_client
{
    public partial class Form1 : Form
    {
        private NotificationListener notificationListener;

        public Form1()
        {
            notificationListener = new NotificationListener();
            notificationListener.OnNotificationReceive += OnNotificationReceive;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadTopics();
        }

        private void subscribeNewTopic_Click(object sender, EventArgs e)
        {
            using (var dialog = new SubscribeDialog())
            {
                var result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    notificationListener.SubscribeToTopic(dialog.getTopicId(), dialog.getServerUrl(), dialog.getUsername(), dialog.getPassword());
                    notificationTopics.Items.Add(dialog.getTopicId());
                    this.SaveTopicsToFile();
                }
            }
        }

        private void removeSelectedTopics_Click(object sender, EventArgs e)
        {
            while (notificationTopics.SelectedIndex > -1)
            {
                var topicId = notificationTopics.Items[notificationTopics.SelectedIndex];
                notificationListener.RemoveTopic((string)topicId);
                notificationTopics.Items.RemoveAt(notificationTopics.SelectedIndex);
            }

            this.SaveTopicsToFile();
        }

        private void notificationTopics_SelectedValueChanged(object sender, EventArgs e)
        {
            removeSelectedTopics.Enabled = notificationTopics.SelectedIndices.Count > 0;
        }

        private void notificationTopics_Click(object sender, EventArgs e)
        {
            var ev = (MouseEventArgs)e;
            var clickedItemIndex = notificationTopics.IndexFromPoint(new Point(ev.X, ev.Y));

            if (clickedItemIndex == -1)
            {
                notificationTopics.ClearSelected();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            var mouseEv = (MouseEventArgs)e;
            if (mouseEv.Button == MouseButtons.Left)
            {
                this.Visible = !this.Visible;
                this.BringToFront();
            }
        }

        private void showControlWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.BringToFront();
        }

        private string GetTopicsFilePath()
        {
            string binaryDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(binaryDirectory, "topics.txt");
        }

        private void SaveTopicsToFile()
        {
            using (StreamWriter writer = new StreamWriter(GetTopicsFilePath()))
            {
                foreach (string topic in notificationTopics.Items)
                {
                    writer.WriteLine(topic);
                }
            }
        }

        private void LoadTopics()
        {
            if (!File.Exists(GetTopicsFilePath())) return;
            using (StreamReader reader = new StreamReader(GetTopicsFilePath()))
            {
                while (!reader.EndOfStream)
                {
                    var topic = reader.ReadLine();
                    notificationListener.SubscribeToTopic(topic, "https://ntfy.sh", null, null);
                    notificationTopics.Items.Add(topic);
                }
            }
        }

        private void OnNotificationReceive(object sender, NotificationReceiveEventArgs e)
        {
            notifyIcon.ShowBalloonTip(3000, e.Title, e.Message, ToolTipIcon.Info);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Dispose();
        }

        private bool trueExit = false;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Let it close
            if (trueExit) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trueExit = true;
            this.Close();
        }

        private void ntfyshWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ntfy.sh/");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new AboutBox();
            d.ShowDialog();
            d.Dispose();
        }
    }
}
