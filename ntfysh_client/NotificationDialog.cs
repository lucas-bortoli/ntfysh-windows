using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using ntfysh_client.Themes;
using Microsoft.Win32;
using System.Media;


namespace ntfysh_client
{
    public partial class NotificationDialog : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool AnimateWindow(IntPtr hWnd, int time, int flags);

        private const int ScreenMargin = 20;

        private int _timeoutSeconds = 0;
        private System.Timers.Timer? _displayTimeoutTimer = null;
        private System.Windows.Forms.Timer? _updateTimer = null;
        private Stopwatch? _shownStopwatch = null;

        private readonly BaseTheme _darkModeTheme = new DarkModeTheme();
        private readonly BaseTheme _defaultTheme = new DefaultTheme();
        private BaseTheme? _theme = null;

        public NotificationDialog()
        {
            ShowInTaskbar = false;
            Visible = false;
            TopMost = true;
            InitializeComponent();
            InitializeWindowHidden();
        }

        public void ShowNotification(string title, string message, int timeoutSeconds = 0, ToolTipIcon? icon = null, bool showTimeOutBar = true, bool showInDarkMode = true, bool playNotificationSound = false)
        {
            if (Visible)
            {
                // close the current notification
                HandleTimeout(null, null);
            }

            _theme = showInDarkMode ? _darkModeTheme : _defaultTheme;

            ApplyTheme();

            // setup data
            IconBox.Image = (icon is null) ? null : ConvertToolTipIconToImage(icon.Value);

            TxBTitle.Text = title;
            TxBMessage.Text = message;

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

            if (timeoutSeconds > 0)
            {
                _displayTimeoutTimer = new System.Timers.Timer(timeoutSeconds * 1000);
                _displayTimeoutTimer.Elapsed += HandleTimeout;
                _displayTimeoutTimer.Start();

                if (showTimeOutBar)
                {
                    ProgressBar1.Value = 100;
                    _updateTimer = new System.Windows.Forms.Timer();
                    _updateTimer.Interval = 100;
                    _updateTimer.Tick += UpdateProgress;
                    _updateTimer.Start();

                    _shownStopwatch = new Stopwatch();
                    _shownStopwatch.Start();

                    ProgressBar1.Visible = true;
                    LblTimeout.Visible = true;
                    _timeoutSeconds = timeoutSeconds;
                }
                else
                {
                    ProgressBar1.Visible = false;
                    LblTimeout.Visible = false;
                }
            }
            else
            {
                ProgressBar1.Visible = false;
                LblTimeout.Visible = false;
            }

            // ok, show the window
            Show();
            ButtonClose.Focus(); // make sure the window is focused, not the title box.
            SetWindowPosition();

            if (playNotificationSound) PlayNotificationSound();
        }

        private void ApplyTheme()
        {
            _theme ??= _defaultTheme;

            // back colors
            BackColor = _theme.BackgroundColor;
            TxBTitle.BackColor = _theme.BackgroundColor;
            TxBMessage.BackColor = _theme.BackgroundColor;
            LblTimeout.BackColor = _theme.BackgroundColor;
            ProgressBar1.BackColor = _theme.BackgroundColor;

            // this one is not "hiding"
            ButtonClose.BackColor = _theme.ControlBackGroundColor;
            ButtonClose.ForeColor = _theme.BackgroundColor;
            // handle mouse over
            ButtonClose.FlatAppearance.MouseOverBackColor = _theme.ControlMouseOverBackgroundColor;

            // fore colors
            ForeColor = _theme.ForegroundColor;
            TxBTitle.ForeColor = _theme.ForegroundColor;
            TxBMessage.ForeColor = _theme.ForegroundColor;
            LblTimeout.ForeColor = _theme.ForegroundColor;
            ProgressBar1.ForeColor = _theme.ForegroundColor;
        }

        private void UpdateProgress(object? sender, EventArgs e)
        {
            if (_shownStopwatch is null) return;

            ProgressBar1.Value = (_timeoutSeconds - _shownStopwatch.Elapsed.Seconds) * 100 / _timeoutSeconds;
            LblTimeout.Text = $@"{_timeoutSeconds - _shownStopwatch.Elapsed.Seconds}";
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
                    flags: NFWinUserAnimateWindowConstants.AW_SLIDE | NFWinUserAnimateWindowConstants.AW_VER_NEGATIVE
                );
            }

            base.SetVisibleCore(value);
        }

        private void SetWindowPosition()
        {
            var workingTop = Screen.PrimaryScreen.WorkingArea.Height - Height;
            Top = workingTop - NotificationDialog.ScreenMargin;

            var workingLeft = Screen.PrimaryScreen.WorkingArea.Width - Width;
            Left = workingLeft - NotificationDialog.ScreenMargin;
        }

        private void UIThreadAnimatedHideWindow(object? sender, EventArgs? e)
        {

            AnimateWindow(
                Handle,
                time: 250,
                flags: NFWinUserAnimateWindowConstants.AW_SLIDE | NFWinUserAnimateWindowConstants.AW_VER_POSITIVE | NFWinUserAnimateWindowConstants.AW_HIDE
            );

            Visible = false;
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
            ButtonClose.Focus();
            Visible = false;
            Opacity = 1;
        }

        private void ButtonClose_ClickHandler(object sender, EventArgs e)
        {
            // don't animate, immediately "close"
            Visible = false;
        }

        private class NFWinUserAnimateWindowConstants
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
                    LblTimeout.Visible = false;
                    ProgressBar1.Visible = false;
                }));
            }
            else
            {
                // in the UI thread, invoke directly
                LblTimeout.Visible = false;
                ProgressBar1.Visible = false;
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

        private void PlayNotificationSound()
        {
            try
            {
                using RegistryKey? defaultSoundPathKey = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\.Default\Notification.Default\.Current");

                if (defaultSoundPathKey is null) throw new Exception();

                if (defaultSoundPathKey.GetValue(null) is not string defaultSoundPath) throw new Exception();

                SoundPlayer loadedSound = new(defaultSoundPath);

                loadedSound.Play();
            }
            catch
            {
                SystemSounds.Beep.Play(); // consolation prize
            }
        }
    }
}
