
namespace ntfysh_client
{
    partial class SettingsDialog
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
            buttonPanel = new System.Windows.Forms.Panel();
            cancelButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            timeoutLabel = new System.Windows.Forms.Label();
            timeout = new System.Windows.Forms.NumericUpDown();
            reconnectAttempts = new System.Windows.Forms.NumericUpDown();
            reconnectAttemptsLabel = new System.Windows.Forms.Label();
            reconnectAttemptDelay = new System.Windows.Forms.NumericUpDown();
            reconnectAttemptDelayLabel = new System.Windows.Forms.Label();
            nativeVersusCustomNotificationsGroupBox = new System.Windows.Forms.GroupBox();
            useCustomTrayNotifications = new System.Windows.Forms.RadioButton();
            useNativeWindowsNotifications = new System.Windows.Forms.RadioButton();
            groupCustomNotificationSettings = new System.Windows.Forms.GroupBox();
            customNotificationsPlayWindowsNotificationAudio = new System.Windows.Forms.CheckBox();
            customNotificationsShowInDarkMode = new System.Windows.Forms.CheckBox();
            customNotificationsShowTimeoutBar = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)timeout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)reconnectAttempts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)reconnectAttemptDelay).BeginInit();
            nativeVersusCustomNotificationsGroupBox.SuspendLayout();
            groupCustomNotificationSettings.SuspendLayout();
            SuspendLayout();
            // 
            // buttonPanel
            // 
            buttonPanel.BackColor = System.Drawing.SystemColors.Control;
            buttonPanel.Controls.Add(cancelButton);
            buttonPanel.Controls.Add(saveButton);
            buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonPanel.Location = new System.Drawing.Point(0, 336);
            buttonPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new System.Drawing.Size(531, 51);
            buttonPanel.TabIndex = 0;
            // 
            // cancelButton
            // 
            cancelButton.Location = new System.Drawing.Point(363, 16);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(444, 16);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.TabIndex = 1;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // timeoutLabel
            // 
            timeoutLabel.AutoSize = true;
            timeoutLabel.Location = new System.Drawing.Point(13, 9);
            timeoutLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeoutLabel.Name = "timeoutLabel";
            timeoutLabel.Size = new System.Drawing.Size(401, 15);
            timeoutLabel.TabIndex = 3;
            timeoutLabel.Text = "Notification Toast Timeout (seconds, use -1 to require closing notification):";
            // 
            // timeout
            // 
            timeout.Location = new System.Drawing.Point(13, 28);
            timeout.Maximum = new decimal(new int[] { -1981284353, -1966660860, 0, 0 });
            timeout.Name = "timeout";
            timeout.Size = new System.Drawing.Size(506, 23);
            timeout.TabIndex = 4;
            // 
            // reconnectAttempts
            // 
            reconnectAttempts.Location = new System.Drawing.Point(12, 73);
            reconnectAttempts.Maximum = new decimal(new int[] { -1981284353, -1966660860, 0, 0 });
            reconnectAttempts.Name = "reconnectAttempts";
            reconnectAttempts.Size = new System.Drawing.Size(506, 23);
            reconnectAttempts.TabIndex = 6;
            // 
            // reconnectAttemptsLabel
            // 
            reconnectAttemptsLabel.AutoSize = true;
            reconnectAttemptsLabel.Location = new System.Drawing.Point(12, 54);
            reconnectAttemptsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            reconnectAttemptsLabel.Name = "reconnectAttemptsLabel";
            reconnectAttemptsLabel.Size = new System.Drawing.Size(287, 15);
            reconnectAttemptsLabel.TabIndex = 5;
            reconnectAttemptsLabel.Text = "Maximum reconnect retry attempts (requires restart):";
            // 
            // reconnectAttemptDelay
            // 
            reconnectAttemptDelay.Location = new System.Drawing.Point(12, 118);
            reconnectAttemptDelay.Maximum = new decimal(new int[] { -1981284353, -1966660860, 0, 0 });
            reconnectAttemptDelay.Name = "reconnectAttemptDelay";
            reconnectAttemptDelay.Size = new System.Drawing.Size(506, 23);
            reconnectAttemptDelay.TabIndex = 8;
            // 
            // reconnectAttemptDelayLabel
            // 
            reconnectAttemptDelayLabel.AutoSize = true;
            reconnectAttemptDelayLabel.Location = new System.Drawing.Point(12, 99);
            reconnectAttemptDelayLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            reconnectAttemptDelayLabel.Name = "reconnectAttemptDelayLabel";
            reconnectAttemptDelayLabel.Size = new System.Drawing.Size(275, 15);
            reconnectAttemptDelayLabel.TabIndex = 7;
            reconnectAttemptDelayLabel.Text = "Delay between attempts (seconds, requires restart):";
            // 
            // nativeVersusCustomNotificationsGroupBox
            // 
            nativeVersusCustomNotificationsGroupBox.Controls.Add(useCustomTrayNotifications);
            nativeVersusCustomNotificationsGroupBox.Controls.Add(useNativeWindowsNotifications);
            nativeVersusCustomNotificationsGroupBox.Location = new System.Drawing.Point(12, 147);
            nativeVersusCustomNotificationsGroupBox.Name = "nativeVersusCustomNotificationsGroupBox";
            nativeVersusCustomNotificationsGroupBox.Size = new System.Drawing.Size(506, 67);
            nativeVersusCustomNotificationsGroupBox.TabIndex = 9;
            nativeVersusCustomNotificationsGroupBox.TabStop = false;
            // 
            // useCustomTrayNotifications
            // 
            useCustomTrayNotifications.AutoSize = true;
            useCustomTrayNotifications.Location = new System.Drawing.Point(6, 40);
            useCustomTrayNotifications.Name = "useCustomTrayNotifications";
            useCustomTrayNotifications.Size = new System.Drawing.Size(267, 19);
            useCustomTrayNotifications.TabIndex = 1;
            useCustomTrayNotifications.TabStop = true;
            useCustomTrayNotifications.Text = "Use ntfysh-windows custom tray notifications";
            useCustomTrayNotifications.UseVisualStyleBackColor = true;
            useCustomTrayNotifications.CheckedChanged += UseCustomTrayNotifications_CheckedChanged;
            // 
            // useNativeWindowsNotifications
            // 
            useNativeWindowsNotifications.AutoSize = true;
            useNativeWindowsNotifications.Location = new System.Drawing.Point(6, 15);
            useNativeWindowsNotifications.Name = "useNativeWindowsNotifications";
            useNativeWindowsNotifications.Size = new System.Drawing.Size(203, 19);
            useNativeWindowsNotifications.TabIndex = 0;
            useNativeWindowsNotifications.TabStop = true;
            useNativeWindowsNotifications.Text = "Use Windows' native notifications";
            useNativeWindowsNotifications.UseVisualStyleBackColor = true;
            // 
            // groupCustomNotificationSettings
            // 
            groupCustomNotificationSettings.Controls.Add(customNotificationsPlayWindowsNotificationAudio);
            groupCustomNotificationSettings.Controls.Add(customNotificationsShowInDarkMode);
            groupCustomNotificationSettings.Controls.Add(customNotificationsShowTimeoutBar);
            groupCustomNotificationSettings.Location = new System.Drawing.Point(12, 243);
            groupCustomNotificationSettings.Name = "groupCustomNotificationSettings";
            groupCustomNotificationSettings.Size = new System.Drawing.Size(504, 87);
            groupCustomNotificationSettings.TabIndex = 10;
            groupCustomNotificationSettings.TabStop = false;
            // 
            // customNotificationsPlayWindowsNotificationAudio
            // 
            customNotificationsPlayWindowsNotificationAudio.AutoSize = true;
            customNotificationsPlayWindowsNotificationAudio.Location = new System.Drawing.Point(4, 59);
            customNotificationsPlayWindowsNotificationAudio.Name = "customNotificationsPlayWindowsNotificationAudio";
            customNotificationsPlayWindowsNotificationAudio.Size = new System.Drawing.Size(200, 19);
            customNotificationsPlayWindowsNotificationAudio.TabIndex = 2;
            customNotificationsPlayWindowsNotificationAudio.Text = "Play Windows notification sound";
            customNotificationsPlayWindowsNotificationAudio.UseVisualStyleBackColor = true;
            // 
            // customNotificationsShowInDarkMode
            // 
            customNotificationsShowInDarkMode.AutoSize = true;
            customNotificationsShowInDarkMode.Location = new System.Drawing.Point(6, 37);
            customNotificationsShowInDarkMode.Name = "customNotificationsShowInDarkMode";
            customNotificationsShowInDarkMode.Size = new System.Drawing.Size(197, 19);
            customNotificationsShowInDarkMode.TabIndex = 1;
            customNotificationsShowInDarkMode.Text = "Show notifications in dark mode";
            customNotificationsShowInDarkMode.UseVisualStyleBackColor = true;
            // 
            // customNotificationsShowTimeoutBar
            // 
            customNotificationsShowTimeoutBar.AutoSize = true;
            customNotificationsShowTimeoutBar.Location = new System.Drawing.Point(6, 14);
            customNotificationsShowTimeoutBar.Name = "customNotificationsShowTimeoutBar";
            customNotificationsShowTimeoutBar.Size = new System.Drawing.Size(211, 19);
            customNotificationsShowTimeoutBar.TabIndex = 0;
            customNotificationsShowTimeoutBar.Text = "Show time-out bar on notifications";
            customNotificationsShowTimeoutBar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 227);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(180, 15);
            label1.TabIndex = 11;
            label1.Text = "Custom tray notification settings";
            // 
            // SettingsDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(531, 387);
            Controls.Add(label1);
            Controls.Add(groupCustomNotificationSettings);
            Controls.Add(nativeVersusCustomNotificationsGroupBox);
            Controls.Add(reconnectAttemptDelay);
            Controls.Add(reconnectAttemptDelayLabel);
            Controls.Add(reconnectAttempts);
            Controls.Add(reconnectAttemptsLabel);
            Controls.Add(timeout);
            Controls.Add(timeoutLabel);
            Controls.Add(buttonPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Settings";
            buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)timeout).EndInit();
            ((System.ComponentModel.ISupportInitialize)reconnectAttempts).EndInit();
            ((System.ComponentModel.ISupportInitialize)reconnectAttemptDelay).EndInit();
            nativeVersusCustomNotificationsGroupBox.ResumeLayout(false);
            nativeVersusCustomNotificationsGroupBox.PerformLayout();
            groupCustomNotificationSettings.ResumeLayout(false);
            groupCustomNotificationSettings.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Label timeoutLabel;
        private System.Windows.Forms.NumericUpDown timeout;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.NumericUpDown reconnectAttempts;
        private System.Windows.Forms.Label reconnectAttemptsLabel;
        private System.Windows.Forms.NumericUpDown reconnectAttemptDelay;
        private System.Windows.Forms.Label reconnectAttemptDelayLabel;
        private System.Windows.Forms.GroupBox nativeVersusCustomNotificationsGroupBox;
        private System.Windows.Forms.RadioButton useCustomTrayNotifications;
        private System.Windows.Forms.RadioButton useNativeWindowsNotifications;
        private System.Windows.Forms.GroupBox groupCustomNotificationSettings;
        private System.Windows.Forms.CheckBox customNotificationsShowTimeoutBar;
        private System.Windows.Forms.CheckBox customNotificationsShowInDarkMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox customNotificationsPlayWindowsNotificationAudio;
    }
}