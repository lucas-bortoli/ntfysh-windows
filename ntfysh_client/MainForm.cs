using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ntfysh_client
{
    public partial class MainForm : Form
    {
        private readonly NotificationListener _notificationListener;
        private bool _trueExit;

        public MainForm(NotificationListener notificationListener)
        {
            _notificationListener = notificationListener;
            _notificationListener.OnNotificationReceive += OnNotificationReceive;
            
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) => LoadTopics();

        private void subscribeNewTopic_Click(object sender, EventArgs e)
        {
            using SubscribeDialog dialog = new SubscribeDialog(notificationTopics);
            DialogResult result = dialog.ShowDialog();

            //Do not subscribe on cancelled dialog
            if (result != DialogResult.OK) return;
                
            //Subscribe
            _notificationListener.SubscribeToTopicUsingLongHttpJson(dialog.Unique, dialog.TopicId, dialog.ServerUrl, dialog.Username, dialog.Password);
                    
            //Add to the user visible list
            notificationTopics.Items.Add(dialog.Unique);
                    
            //Save the topics persistently
            SaveTopicsToFile();
        }

        private async void removeSelectedTopics_Click(object sender, EventArgs e)
        {
            while (notificationTopics.SelectedIndex > -1)
            {
                string topicUniqueString = (string)notificationTopics.Items[notificationTopics.SelectedIndex];
                
                await _notificationListener.UnsubscribeFromTopicAsync(topicUniqueString);
                notificationTopics.Items.Remove(topicUniqueString);
            }

            SaveTopicsToFile();
        }

        private void notificationTopics_SelectedValueChanged(object sender, EventArgs e)
        {
            removeSelectedTopics.Enabled = notificationTopics.SelectedIndices.Count > 0;
        }

        private void notificationTopics_Click(object sender, EventArgs e)
        {
            MouseEventArgs ev = (MouseEventArgs)e;
            int clickedItemIndex = notificationTopics.IndexFromPoint(new Point(ev.X, ev.Y));

            if (clickedItemIndex == -1) notificationTopics.ClearSelected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEv = (MouseEventArgs)e;
            
            if (mouseEv.Button != MouseButtons.Left) return;
            
            Visible = !Visible;
            BringToFront();
        }

        private void showControlWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
            BringToFront();
        }

        private string GetTopicsFilePath()
        {
            string binaryDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("Unable to determine path for application");
            return Path.Combine(binaryDirectory ?? throw new InvalidOperationException("Unable to determine path for topics file"), "topics.json");
        }
        
        private string GetLegacyTopicsFilePath()
        {
            string binaryDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("Unable to determine path for application");
            return Path.Combine(binaryDirectory ?? throw new InvalidOperationException("Unable to determine path for legacy topics file"), "topics.txt");
        }

        private void SaveTopicsToFile()
        {
            string topicsSerialised = JsonConvert.SerializeObject(_notificationListener.SubscribedTopicsByUnique.Select(st => st.Value).ToList(), Formatting.Indented);
            
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
                        string? legacyTopic = reader.ReadLine();
                        if (!string.IsNullOrWhiteSpace(legacyTopic)) legacyTopics.Add(legacyTopic);
                    }
                }

                //Assemble new format
                List<SubscribedTopic> newTopics = legacyTopics.Select(lt => new SubscribedTopic(lt, "https://ntfy.sh", null, null, null, null)).ToList();

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
            List<SubscribedTopic>? topics = JsonConvert.DeserializeObject<List<SubscribedTopic>>(topicsSerialised);

            if (topics is null)
            {
                //TODO Deserialise error!
                return;
            }
            
            //Load them in
            foreach (SubscribedTopic topic in topics)
            {
                _notificationListener.SubscribeToTopicUsingLongHttpJson($"{topic.TopicId}@{topic.ServerUrl}", topic.TopicId, topic.ServerUrl, topic.Username, topic.Password);
                notificationTopics.Items.Add($"{topic.TopicId}@{topic.ServerUrl}");
            }
        }

        private void OnNotificationReceive(object sender, NotificationReceiveEventArgs e)
        {
            notifyIcon.ShowBalloonTip(3000, e.Title, e.Message, ToolTipIcon.Info);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Dispose();
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Let it close
            if (_trueExit) return;

            if (e.CloseReason != CloseReason.UserClosing) return;
            
            Visible = false;
            e.Cancel = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _trueExit = true;
            Close();
        }

        private void ntfyshWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://ntfy.sh/")
            {
                UseShellExecute = true
            });
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using AboutBox d = new AboutBox();
            d.ShowDialog();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _trueExit = true;
            Close();
        }
    }
}
