using System;
using System.Windows.Forms;

namespace ntfysh_client
{
    public partial class SettingsDialog : Form
    {
        public decimal Timeout
        {
            get => timeout.Value;
            set => timeout.Value = value;
        }

        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
