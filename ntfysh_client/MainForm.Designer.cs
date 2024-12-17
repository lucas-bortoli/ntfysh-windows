
namespace ntfysh_client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            subscribeNewTopic = new System.Windows.Forms.Button();
            removeSelectedTopics = new System.Windows.Forms.Button();
            notificationTopics = new System.Windows.Forms.ListBox();
            topicContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            sendNotificationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            notifyIcon = new System.Windows.Forms.NotifyIcon(components);
            trayContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            showControlWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ntfyshWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            toolTip = new System.Windows.Forms.ToolTip(components);
            topicContextMenu.SuspendLayout();
            trayContextMenu.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // subscribeNewTopic
            // 
            subscribeNewTopic.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            subscribeNewTopic.Location = new System.Drawing.Point(211, 251);
            subscribeNewTopic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            subscribeNewTopic.Name = "subscribeNewTopic";
            subscribeNewTopic.Size = new System.Drawing.Size(188, 27);
            subscribeNewTopic.TabIndex = 2;
            subscribeNewTopic.Text = "Add";
            subscribeNewTopic.UseVisualStyleBackColor = true;
            subscribeNewTopic.Click += subscribeNewTopic_Click;
            // 
            // removeSelectedTopics
            // 
            removeSelectedTopics.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            removeSelectedTopics.Enabled = false;
            removeSelectedTopics.Location = new System.Drawing.Point(13, 251);
            removeSelectedTopics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            removeSelectedTopics.Name = "removeSelectedTopics";
            removeSelectedTopics.Size = new System.Drawing.Size(188, 27);
            removeSelectedTopics.TabIndex = 0;
            removeSelectedTopics.Text = "Remove selected";
            removeSelectedTopics.UseVisualStyleBackColor = true;
            removeSelectedTopics.Click += removeSelectedTopics_Click;
            // 
            // notificationTopics
            // 
            notificationTopics.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            notificationTopics.ContextMenuStrip = topicContextMenu;
            notificationTopics.FormattingEnabled = true;
            notificationTopics.ItemHeight = 15;
            notificationTopics.Location = new System.Drawing.Point(13, 46);
            notificationTopics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            notificationTopics.Name = "notificationTopics";
            notificationTopics.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            notificationTopics.Size = new System.Drawing.Size(386, 199);
            notificationTopics.TabIndex = 3;
            toolTip.SetToolTip(notificationTopics, "Double click topic to send message");
            notificationTopics.Click += notificationTopics_Click;
            notificationTopics.SelectedValueChanged += notificationTopics_SelectedValueChanged;
            notificationTopics.MouseDoubleClick += notificationTopics_MouseDoubleClick;
            // 
            // topicContextMenu
            // 
            topicContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { sendNotificationMenuItem });
            topicContextMenu.Name = "topicContextMenu";
            topicContextMenu.Size = new System.Drawing.Size(167, 26);
            // 
            // sendNotificationMenuItem
            // 
            sendNotificationMenuItem.Name = "sendNotificationMenuItem";
            sendNotificationMenuItem.Size = new System.Drawing.Size(166, 22);
            sendNotificationMenuItem.Text = "Send Notification";
            sendNotificationMenuItem.Click += SendNotificationMenuItem_Click;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = trayContextMenu;
            notifyIcon.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "ntfy.sh";
            notifyIcon.Visible = true;
            notifyIcon.Click += notifyIcon_Click;
            // 
            // trayContextMenu
            // 
            trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { showControlWindowToolStripMenuItem, exitToolStripMenuItem });
            trayContextMenu.Name = "trayContextMenu";
            trayContextMenu.Size = new System.Drawing.Size(190, 48);
            // 
            // showControlWindowToolStripMenuItem
            // 
            showControlWindowToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("showControlWindowToolStripMenuItem.Image");
            showControlWindowToolStripMenuItem.Name = "showControlWindowToolStripMenuItem";
            showControlWindowToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            showControlWindowToolStripMenuItem.Text = "Show control window";
            showControlWindowToolStripMenuItem.Click += showControlWindowToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("exitToolStripMenuItem.Image");
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.Color.White;
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(412, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exitToolStripMenuItem1, settingsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Image = (System.Drawing.Image)resources.GetObject("exitToolStripMenuItem1.Image");
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            exitToolStripMenuItem1.Text = "Exit";
            exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("settingsToolStripMenuItem.Image");
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ntfyshWebsiteToolStripMenuItem, toolStripMenuItem1, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // ntfyshWebsiteToolStripMenuItem
            // 
            ntfyshWebsiteToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("ntfyshWebsiteToolStripMenuItem.Image");
            ntfyshWebsiteToolStripMenuItem.Name = "ntfyshWebsiteToolStripMenuItem";
            ntfyshWebsiteToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            ntfyshWebsiteToolStripMenuItem.Text = "Open ntfy.sh website";
            ntfyshWebsiteToolStripMenuItem.Click += ntfyshWebsiteToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(182, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("aboutToolStripMenuItem.Image");
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 27);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(171, 15);
            label1.TabIndex = 1;
            label1.Text = "Subscribed Notification Topics:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(412, 288);
            Controls.Add(menuStrip1);
            Controls.Add(notificationTopics);
            Controls.Add(removeSelectedTopics);
            Controls.Add(subscribeNewTopic);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ntfy.sh";
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            topicContextMenu.ResumeLayout(false);
            trayContextMenu.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button subscribeNewTopic;
        private System.Windows.Forms.Button removeSelectedTopics;
        private System.Windows.Forms.ListBox notificationTopics;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem showControlWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ntfyshWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip topicContextMenu;
        private System.Windows.Forms.ToolStripMenuItem sendNotificationMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

