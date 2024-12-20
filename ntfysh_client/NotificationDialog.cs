﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ntfysh_client
{
    public partial class NotificationDialog : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool AnimateWindow(IntPtr hWnd, int time, int flags);

        private const int ScreenMargin = 20;

        private System.Timers.Timer? timer = null;

        private void SetWindowPosition()
        {
            int workingtop = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Top = workingtop - NotificationDialog.ScreenMargin;

            int workingleft = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Left = workingleft - NotificationDialog.ScreenMargin;
        }

        protected override void SetVisibleCore(bool value)
        {

            //base.SetVisibleCore(false);
            this.SetWindowPosition();
            if (value)
            {
                this.BringToFront();
                AnimateWindow(this.Handle, 250, 0x00040000 | 0x00000008);
            }
            base.SetVisibleCore(value);
        }

        private void ui_hide_window(object? sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 250, 0x00040000 | 0x00000004 | 0x00010000);
            this.IsVisible = false;
        }

        private void handleTimeout(object? sender, EventArgs e)
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Dispose();
                this.timer = null;
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.ui_hide_window(sender, e)));
            }
            else
            {
                this.ui_hide_window(sender, e);
            }
        }

        public void ShowNotification(string title, string message, int timeout_ms=-1)
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Dispose();
            }
            if (timeout_ms > 0)
            {
                this.timer = new System.Timers.Timer(timeout_ms);
                timer.Elapsed += handleTimeout;
                this.timer.Start();
            }
            this.tbTitle.Text = title;
            this.tbMessage.Text = message;
            this.Show();
            this.SetWindowPosition();
        }

        public bool IsVisible
        {
            get { return this.Visible; }
            set { this.Visible = value; }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        public NotificationDialog()
        {
            this.IsVisible = false;
            this.TopMost = true;
            InitializeComponent();
            InitializeWindowHidden();
        }

        private void InitializeWindowHidden()
        {
            this.Opacity = 0;
            this.ShowNotification("Title", "Message");
            this.IsVisible = false;
            this.Opacity = 1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // immediate
            this.IsVisible = false;
        }
    }
}
