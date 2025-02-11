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
            TxBTitle = new System.Windows.Forms.TextBox();
            ButtonClose = new System.Windows.Forms.Button();
            TxBMessage = new System.Windows.Forms.RichTextBox();
            IconBox = new System.Windows.Forms.PictureBox();
            ProgressBar1 = new System.Windows.Forms.ProgressBar();
            LblTimeout = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)IconBox).BeginInit();
            SuspendLayout();
            // 
            // TxBTitle
            // 
            TxBTitle.BackColor = System.Drawing.SystemColors.ControlDark;
            TxBTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            TxBTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxBTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            TxBTitle.Location = new System.Drawing.Point(54, 13);
            TxBTitle.Name = "TxBTitle";
            TxBTitle.ReadOnly = true;
            TxBTitle.Size = new System.Drawing.Size(683, 32);
            TxBTitle.TabIndex = 0;
            TxBTitle.MouseDown += window_MouseDown;
            // 
            // ButtonClose
            // 
            ButtonClose.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            ButtonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            ButtonClose.FlatAppearance.BorderSize = 0;
            ButtonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ButtonClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            ButtonClose.Location = new System.Drawing.Point(759, 7);
            ButtonClose.Name = "ButtonClose";
            ButtonClose.Size = new System.Drawing.Size(29, 38);
            ButtonClose.TabIndex = 1;
            ButtonClose.Text = "X";
            ButtonClose.UseVisualStyleBackColor = false;
            ButtonClose.Click += ButtonClose_ClickHandler;
            // 
            // TxBMessage
            // 
            TxBMessage.BackColor = System.Drawing.SystemColors.ControlDark;
            TxBMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            TxBMessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            TxBMessage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            TxBMessage.Location = new System.Drawing.Point(12, 57);
            TxBMessage.Name = "TxBMessage";
            TxBMessage.ReadOnly = true;
            TxBMessage.Size = new System.Drawing.Size(776, 191);
            TxBMessage.TabIndex = 2;
            TxBMessage.Text = "";
            TxBMessage.MouseDown += window_MouseDown;
            // 
            // IconBox
            // 
            IconBox.Location = new System.Drawing.Point(12, 12);
            IconBox.Name = "IconBox";
            IconBox.Size = new System.Drawing.Size(36, 39);
            IconBox.TabIndex = 3;
            IconBox.TabStop = false;
            // 
            // ProgressBar1
            // 
            ProgressBar1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            ProgressBar1.Enabled = false;
            ProgressBar1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            ProgressBar1.Location = new System.Drawing.Point(70, 254);
            ProgressBar1.MarqueeAnimationSpeed = 1;
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new System.Drawing.Size(718, 23);
            ProgressBar1.Step = 1;
            ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            ProgressBar1.TabIndex = 4;
            ProgressBar1.Value = 100;
            // 
            // LblTimeout
            // 
            LblTimeout.AutoSize = true;
            LblTimeout.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblTimeout.Location = new System.Drawing.Point(21, 254);
            LblTimeout.Name = "LblTimeout";
            LblTimeout.Size = new System.Drawing.Size(43, 17);
            LblTimeout.TabIndex = 5;
            LblTimeout.Text = "label1";
            LblTimeout.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // NotificationDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlDark;
            ClientSize = new System.Drawing.Size(800, 289);
            Controls.Add(LblTimeout);
            Controls.Add(ProgressBar1);
            Controls.Add(IconBox);
            Controls.Add(TxBMessage);
            Controls.Add(ButtonClose);
            Controls.Add(TxBTitle);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "NotificationDialog";
            Text = "NotificationDialog";
            Click += window_MouseDown;
            ((System.ComponentModel.ISupportInitialize)IconBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox TxBTitle;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.RichTextBox TxBMessage;
        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.ProgressBar ProgressBar1;
        private System.Windows.Forms.Label LblTimeout;
    }
}