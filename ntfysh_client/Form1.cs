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
using Newtonsoft.Json;

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
            using (var dialog = new SubscribeDialog(notificationTopics))
            {
                var result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    notificationListener.SubscribeToTopic(dialog.getUniqueString(), dialog.getTopicId(), dialog.getServerUrl(), dialog.getUsername(), dialog.getPassword());
                    notificationTopics.Items.Add(dialog.getUniqueString());
                    this.SaveTopicsToFile();
                }
            }
        }

        private void removeSelectedTopics_Click(object sender, EventArgs e)
        {
            while (notificationTopics.SelectedIndex > -1)
            {
                string topicUniqueString = (string)notificationTopics.Items[notificationTopics.SelectedIndex];
                
                notificationListener.RemoveTopicByUniqueString(topicUniqueString);
                notificationTopics.Items.Remove(topicUniqueString);
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
            string binaryDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(binaryDirectory ?? throw new InvalidOperationException("Unable to determine path for topics file"), "topics.json");
        }
        
        private string GetLegacyTopicsFilePath()
        {
            string binaryDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(binaryDirectory ?? throw new InvalidOperationException("Unable to determine path for legacy topics file"), "topics.txt");
        }

        private void SaveTopicsToFile()
        {
            string topicsSerialised = JsonConvert.SerializeObject(notificationListener.SubscribedTopicsByUnique.Select(st => st.Value).ToList(), Formatting.Indented);
            
            File.WriteAllText(GetTopicsFilePath(), topicsSerialised);
        }

        private void LoadTopics()
        {
            string legacyTopicsPath = GetLegacyTopicsFilePath();
            string topicsFilePath = GetTopicsFilePath();

            //If we have an old format topics file. Convert it to the new format!
            if (File.Exists(legacyTopicsPath))
            {
                //Read old format
                List<string> legacyTopics = new List<string>();
                
                using (StreamReader reader = new StreamReader(legacyTopicsPath))
                {
                    while (!reader.EndOfStream)
                    {
                        string legacyTopic = reader.ReadLine();
                        legacyTopics.Add(legacyTopic);
                    }
                }

                //Assemble new format
                List<SubscribedTopic> newTopics = legacyTopics.Select(lt => new SubscribedTopic(lt, "https://ntfy.sh", null, null, null)).ToList();

                string newFormatSerialised = JsonConvert.SerializeObject(newTopics, Formatting.Indented);
                
                //Write new format
                File.WriteAllText(topicsFilePath, newFormatSerialised);
                
                //Delete old format
                File.Delete(legacyTopicsPath);
            }
            
            //Check if we have any topics file on disk to load
            if (!File.Exists(topicsFilePath)) return;
            
            //We have a topics file. Load it!
            string topicsSerialised = File.ReadAllText(topicsFilePath);

            //Check if the file is empty
            if (string.IsNullOrWhiteSpace(topicsSerialised))
            {
                //The file is empty. May as well remove it and consider it nonexistent
                File.Delete(topicsFilePath);
                return;
            }

            //Deserialise the topics
            List<SubscribedTopic> topics = JsonConvert.DeserializeObject<List<SubscribedTopic>>(topicsSerialised);

            if (topics == null)
            {
                //TODO Deserialise error!
                return;
            }
            
            //Load them in
            foreach (SubscribedTopic topic in topics)
            {
                notificationListener.SubscribeToTopic($"{topic.TopicId}@{topic.ServerUrl}", topic.TopicId, topic.ServerUrl, topic.Username, topic.Password);
                notificationTopics.Items.Add($"{topic.TopicId}@{topic.ServerUrl}");
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
