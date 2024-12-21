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
        private System.Timers.Timer? _displayTimeoutTimer = null;
        private System.Windows.Forms.Timer? _updateTimer = null;
        private Stopwatch? _shownStopwatch = null;
        private ToolTipIcon? _icon;
        private int _progress_value = 0;
        private int progress
        {
            get { return _progress_value; }
            set
            {
                _progress_value = value;
                progressBar1.Value = value;
            }
        }

        public bool IsVisible
        {
            get { return Visible; }
            set { Visible = value; }
        }

        public NotificationDialog()
        {
            ShowInTaskbar = false;
            IsVisible = false;
            TopMost = true;
            InitializeComponent();
            InitializeWindowHidden();
        }

        public void ShowNotification(string title, string message, int timeout_ms = -1, ToolTipIcon? icon = null, bool showTimeOutBar = true, bool showInDarkMode = true)
        {
            if (IsVisible)
            {
                // close the current notification
                HandleTimeout(null, null);
            }

            // setup data
            _icon = icon;

            if (_icon != null)
            {
                iconBox.Image = ConvertToolTipIconToImage(_icon.Value);
            }

            tbTitle.Text = title;
            tbMessage.Text = message;

            // setup timers
            if (_displayTimeoutTimer != null)
            {
                _displayTimeoutTimer.Stop();
                _displayTimeoutTimer.Dispose();
            }

            if (_updateTimer != null)
            {
                _updateTimer.Stop();
                _updateTimer.Dispose();
            }

            if (timeout_ms > 0)
            {
                _displayTimeoutTimer = new System.Timers.Timer(timeout_ms);
                _displayTimeoutTimer.Elapsed += HandleTimeout;
                _displayTimeoutTimer.Start();

                if (showTimeOutBar)
                {
                    progress = 100;
                    _updateTimer = new System.Windows.Forms.Timer();
                    _updateTimer.Interval = 100;
                    _updateTimer.Tick += UpdateProgress;
                    _updateTimer.Start();

                    _shownStopwatch = new Stopwatch();
                    _shownStopwatch.Start();

                    progressBar1.Visible = true;
                    lbTimeout.Visible = true;
                    _timeout = timeout_ms;
                }
                else
                {
                    progressBar1.Visible = false;
                    lbTimeout.Visible = false;
                }
            }
            else
            {
                progressBar1.Visible = false;
                lbTimeout.Visible = false;
            }

            // ok, show the window
            Show();
            SetWindowPosition();
        }

        private void UpdateProgress(object? sender, EventArgs e)
        {
            if (_shownStopwatch is null) return;

            progress = (int)((_timeout - _shownStopwatch.ElapsedMilliseconds) * 100 / _timeout);
            lbTimeout.Text = $"{(int)(_timeout - _shownStopwatch.ElapsedMilliseconds) / 1000}";
        }

        protected override void SetVisibleCore(bool value)
        {
            SetWindowPosition();

            if (value)
            {
                BringToFront();

                AnimateWindow(
                    Handle,
                    time: 250,
                    flags: NFWinUserAnimateWindowConstnats.AW_SLIDE | NFWinUserAnimateWindowConstnats.AW_VER_NEGATIVE
                );
            }

            base.SetVisibleCore(value);
        }

        private void SetWindowPosition()
        {
            int workingtop = Screen.PrimaryScreen.WorkingArea.Height - Height;
            Top = workingtop - NotificationDialog.ScreenMargin;

            int workingleft = Screen.PrimaryScreen.WorkingArea.Width - Width;
            Left = workingleft - NotificationDialog.ScreenMargin;
        }

        private void UIThreadAnimatedHideWindow(object? sender, EventArgs? e)
        {

            AnimateWindow(
                Handle,
                time: 250,
                flags: NFWinUserAnimateWindowConstnats.AW_SLIDE | NFWinUserAnimateWindowConstnats.AW_VER_POSITIVE | NFWinUserAnimateWindowConstnats.AW_HIDE
            );

            IsVisible = false;
        }

        private void HandleTimeout(object? sender, EventArgs? e)
        {
            CancelTimer();

            if (InvokeRequired)
            {
                // on a background thread, so invoke on the UI thread
                Invoke(new Action(() => UIThreadAnimatedHideWindow(sender, e)));
            }
            else
            {
                // in the UI thread, invoke directly
                UIThreadAnimatedHideWindow(sender, e);
            }
        }

        private Image? ConvertToolTipIconToImage(ToolTipIcon icon) => icon switch
        {
            ToolTipIcon.Info => SystemIcons.Information.ToBitmap(),
            ToolTipIcon.Warning => SystemIcons.Warning.ToBitmap(),
            ToolTipIcon.Error => SystemIcons.Error.ToBitmap(),
            _ => null
        };

        private void InitializeWindowHidden()
        {
            Opacity = 0;
            ShowNotification("Title", "Message");
            IsVisible = false;
            Opacity = 1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // don't animate, immediately "close"
            IsVisible = false;
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

        private void window_MouseDown(object sender, EventArgs e) => CancelTimer();
        
        private void CancelTimer()
        {
            if (InvokeRequired)
            {
                // on a background thread, so invoke on the UI thread
                Invoke(new Action(() =>
                {
                    lbTimeout.Visible = false;
                    progressBar1.Visible = false;
                }));
            }
            else
            {
                // in the UI thread, invoke directly
                lbTimeout.Visible = false;
                progressBar1.Visible = false;
            }

            if (_displayTimeoutTimer != null) // check if the timer has already been disposed
            {
                _displayTimeoutTimer.Stop();
                _displayTimeoutTimer.Dispose();
                _displayTimeoutTimer = null;
            }

            if (_updateTimer != null)
            {
                _updateTimer.Stop();
                _updateTimer.Dispose();
                _updateTimer = null;
            }

            if (_shownStopwatch != null)
            {
                _shownStopwatch.Stop();
                _shownStopwatch = null;
            }
        }
    }
}
