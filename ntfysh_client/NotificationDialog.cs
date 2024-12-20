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

        public void ShowNotification(string title, string message)
        {
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
            this.IsVisible = false;
        }
    }
}
