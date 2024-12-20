using System;
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
        private ToolTipIcon? _icon;

        public bool IsVisible
        {
            get { return this.Visible; }
            set { this.Visible = value; }
        }

        public NotificationDialog()
        {
            this.IsVisible = false;
            this.TopMost = true;
            InitializeComponent();
            InitializeWindowHidden();
        }

        public void ShowNotification(string title, string message, int timeout_ms = -1, ToolTipIcon? icon = null)
        {
            this._icon = icon;
            if (this._icon != null)
            {
                this.iconBox.Image = ConvertToolTipIconToImage(_icon.Value);
            }
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

        protected override void SetVisibleCore(bool value)
        {
            this.SetWindowPosition();
            if (value)
            {
                this.BringToFront();
                AnimateWindow(
                    this.Handle,
                    time: 250,
                    flags: NFWinUserAnimateWindowConstnats.AW_SLIDE | NFWinUserAnimateWindowConstnats.AW_VER_NEGATIVE
                );
            }
            base.SetVisibleCore(value);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void SetWindowPosition()
        {
            int workingtop = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Top = workingtop - NotificationDialog.ScreenMargin;

            int workingleft = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Left = workingleft - NotificationDialog.ScreenMargin;
        }

        private void ui_hide_window(object? sender, EventArgs e)
        {
            AnimateWindow(
                this.Handle,
                time: 250,
                flags: NFWinUserAnimateWindowConstnats.AW_SLIDE | NFWinUserAnimateWindowConstnats.AW_VER_POSITIVE | NFWinUserAnimateWindowConstnats.AW_HIDE
            );
            this.IsVisible = false;
        }

        private void handleTimeout(object? sender, EventArgs e)
        {
            if (this.timer != null) // check if the timer has already been disposed
            {
                this.timer.Stop();
                this.timer.Dispose();
                this.timer = null;
            }
            if (this.InvokeRequired)
            {
                // on a background thread, so invoke on the UI thread
                this.Invoke(new Action(() => this.ui_hide_window(sender, e)));
            }
            else
            {
                // in the UI thread, invoke directly
                this.ui_hide_window(sender, e);
            }
        }

        private Image? ConvertToolTipIconToImage(ToolTipIcon icon)
        {
            switch (icon)
            {
                case ToolTipIcon.Info:
                    return SystemIcons.Information.ToBitmap();
                case ToolTipIcon.Warning:
                    return SystemIcons.Warning.ToBitmap();
                case ToolTipIcon.Error:
                    return SystemIcons.Error.ToBitmap();
                case ToolTipIcon.None:
                default:
                    return null;
            }
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

        private class NFWinUserAnimateWindowConstnats
        {
            public const int AW_HOR_POSITIVE = 0x00000001;
            public const int AW_HOR_NEGATIVE = 0x00000002;
            public const int AW_VER_POSITIVE = 0x00000004;
            public const int AW_VER_NEGATIVE = 0x00000008;
            public const int AW_CENTER = 0x00000010;
            public const int AW_HIDE = 0x00010000;
            public const int AW_ACTIVATE = 0x00020000;
            public const int AW_SLIDE = 0x00040000;
            public const int AW_BLEND = 0x00080000;
        }
    }
}
