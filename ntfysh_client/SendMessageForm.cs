using System.Windows.Forms;

namespace ntfysh_client
{
    public partial class SendMessageForm : Form
    {
        public string Message => richTextBox.Text;
        public string Title => textBox.Text;

        public SendMessageForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
