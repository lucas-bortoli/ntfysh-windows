﻿namespace ntfysh_client
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
            tbMessage = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // tbTitle
            // 
            tbTitle.Location = new System.Drawing.Point(12, 58);
            tbTitle.Name = "tbTitle";
            tbTitle.ReadOnly = true;
            tbTitle.Size = new System.Drawing.Size(725, 23);
            tbTitle.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(759, 7);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(29, 38);
            button1.TabIndex = 1;
            button1.Text = "x";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnClose_Click;
            // 
            // tbMessage
            // 
            tbMessage.Location = new System.Drawing.Point(12, 97);
            tbMessage.Multiline = true;
            tbMessage.Name = "tbMessage";
            tbMessage.ReadOnly = true;
            tbMessage.Size = new System.Drawing.Size(725, 253);
            tbMessage.TabIndex = 2;
            // 
            // NotificationDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlDark;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(tbMessage);
            Controls.Add(button1);
            Controls.Add(tbTitle);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "NotificationDialog";
            Text = "NotificationDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbMessage;
    }
}