using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Win32;
using System.Diagnostics;


namespace ntfysh_client
{
    public partial class NotificationDialog : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool AnimateWindow(IntPtr hWnd, int time, int flags);

        private const int ScreenMargin = 20;

        private int _timeout = 0;
        private System.Timers.Timer? displayTimeoutTimer = null;
        private System.Windows.Forms.Timer? updateTimer = null;
        private Stopwatch? shownStopwatch = null;
        private ToolTipIcon? _icon;
        private int _progress_value = 0;
        private int progress
        {
            get { return this._progress_value; }
            set
            {
                this._progress_value = value;
                this.progressBar1.Value = value;
            }
        }

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
            if (this.IsVisible)
            {
                // close the current notification
                this.handleTimeout(null, null);
            }
            // setup data
            this._icon = icon;
            if (this._icon != null)
            {
                this.iconBox.Image = ConvertToolTipIconToImage(_icon.Value);
            }
            this.tbTitle.Text = title;
            this.tbMessage.Text = message;

            // setup timers
            if (this.displayTimeoutTimer != null)
            {
                this.displayTimeoutTimer.Stop();
                this.displayTimeoutTimer.Dispose();
            }
            if (this.updateTimer != null)
            {
                this.updateTimer.Stop();
                this.updateTimer.Dispose();
            }
            if (timeout_ms > 0)
            {
                this.displayTimeoutTimer = new System.Timers.Timer(timeout_ms);
                displayTimeoutTimer.Elapsed += handleTimeout;
                this.displayTimeoutTimer.Start();

                this.progress = 100;
                this.updateTimer = new System.Windows.Forms.Timer();
                updateTimer.Interval = 100;
                this.updateTimer.Tick += this.UpdateProgress;
                this.updateTimer.Start();

                this.shownStopwatch = new Stopwatch();
                this.shownStopwatch.Start();

                this.progressBar1.Visible = true;
                this.lbTimeout.Visible = true;
                this._timeout = timeout_ms;
            }
            else
            {
                this.progressBar1.Visible = false;
                this.lbTimeout.Visible = false;
            }

            // ok, show the window
            this.Show();
            this.SetWindowPosition();
        }

        private void UpdateProgress(object? sender, EventArgs e)
        {
            if (this.shownStopwatch == null)
            {
                return;
            }
            this.progress = (int)((this._timeout - this.shownStopwatch.ElapsedMilliseconds) * 100 / this._timeout);
            this.lbTimeout.Text = $"{(int)(this._timeout - this.shownStopwatch.ElapsedMilliseconds) / 1000}";
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

        private void ui_hide_window(object? sender, EventArgs? e)
        {
            AnimateWindow(
                this.Handle,
                time: 250,
                flags: NFWinUserAnimateWindowConstnats.AW_SLIDE | NFWinUserAnimateWindowConstnats.AW_VER_POSITIVE | NFWinUserAnimateWindowConstnats.AW_HIDE
            );
            this.IsVisible = false;
        }

        private void handleTimeout(object? sender, EventArgs? e)
        {
            this.cancelTimer();
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

        private void window_MouseDown(object sender, EventArgs e)
        {
            this.cancelTimer();
        }

        private void cancelTimer()
        {
            if (this.InvokeRequired)
            {
                // on a background thread, so invoke on the UI thread
                this.Invoke(new Action(() =>
                {
                    this.lbTimeout.Visible = false;
                    this.progressBar1.Visible = false;
                }));
            }
            else
            {
                // in the UI thread, invoke directly
                this.lbTimeout.Visible = false;
                this.progressBar1.Visible = false;
            }

            if (this.displayTimeoutTimer != null) // check if the timer has already been disposed
            {
                this.displayTimeoutTimer.Stop();
                this.displayTimeoutTimer.Dispose();
                this.displayTimeoutTimer = null;
            }
            if (this.updateTimer != null)
            {
                this.updateTimer.Stop();
                this.updateTimer.Dispose();
                this.updateTimer = null;
            }
            if (this.shownStopwatch != null)
            {
                this.shownStopwatch.Stop();
                this.shownStopwatch = null;
            }
        }
    }
}
