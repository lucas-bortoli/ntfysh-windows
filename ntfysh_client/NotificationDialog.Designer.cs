namespace ntfysh_client
{
    partial class NotificationDialog
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
            tbTitle = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            tbMessage = new System.Windows.Forms.RichTextBox();
            iconBox = new System.Windows.Forms.PictureBox();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            lbTimeout = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)iconBox).BeginInit();
            SuspendLayout();
            // 
            // tbTitle
            // 
            tbTitle.BackColor = System.Drawing.SystemColors.ControlDark;
            tbTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tbTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            tbTitle.Location = new System.Drawing.Point(54, 13);
            tbTitle.Name = "tbTitle";
            tbTitle.ReadOnly = true;
            tbTitle.Size = new System.Drawing.Size(683, 32);
            tbTitle.TabIndex = 0;
            tbTitle.MouseDown += window_MouseDown;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            button1.Location = new System.Drawing.Point(759, 7);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(29, 38);
            button1.TabIndex = 1;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnClose_Click;
            // 
            // tbMessage
            // 
            tbMessage.BackColor = System.Drawing.SystemColors.ControlDark;
            tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbMessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tbMessage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            tbMessage.Location = new System.Drawing.Point(12, 57);
            tbMessage.Name = "tbMessage";
            tbMessage.ReadOnly = true;
            tbMessage.Size = new System.Drawing.Size(776, 191);
            tbMessage.TabIndex = 2;
            tbMessage.Text = "";
            tbMessage.MouseDown += window_MouseDown;
            // 
            // iconBox
            // 
            iconBox.Location = new System.Drawing.Point(12, 12);
            iconBox.Name = "iconBox";
            iconBox.Size = new System.Drawing.Size(36, 39);
            iconBox.TabIndex = 3;
            iconBox.TabStop = false;
            // 
            // progressBar1
            // 
            progressBar1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            progressBar1.Enabled = false;
            progressBar1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            progressBar1.Location = new System.Drawing.Point(70, 254);
            progressBar1.MarqueeAnimationSpeed = 1;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(718, 23);
            progressBar1.Step = 1;
            progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 4;
            progressBar1.Value = 100;
            // 
            // lbTimeout
            // 
            lbTimeout.AutoSize = true;
            lbTimeout.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lbTimeout.Location = new System.Drawing.Point(21, 254);
            lbTimeout.Name = "lbTimeout";
            lbTimeout.Size = new System.Drawing.Size(43, 17);
            lbTimeout.TabIndex = 5;
            lbTimeout.Text = "label1";
            lbTimeout.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // NotificationDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlDark;
            ClientSize = new System.Drawing.Size(800, 289);
            Controls.Add(lbTimeout);
            Controls.Add(progressBar1);
            Controls.Add(iconBox);
            Controls.Add(tbMessage);
            Controls.Add(button1);
            Controls.Add(tbTitle);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "NotificationDialog";
            Text = "NotificationDialog";
            Click += window_MouseDown;
            ((System.ComponentModel.ISupportInitialize)iconBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox tbMessage;
        private System.Windows.Forms.PictureBox iconBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbTimeout;
    }
}