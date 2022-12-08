using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ntfysh_client.Notifications;

namespace ntfysh_client
{
    static class Program
    {
        private static readonly NotificationListener NotificationListener = new();
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            args = args.Select(a => a.ToLower()).ToArray();

            if (args.Contains("-h") || args.Contains("--help"))
            {
                MessageBox.Show("Help:\n    -h\n    --help\n\nStart in tray:\n    -t\n    --start-in-tray\n\nAllow multiple instances:\n    -m\n    --allow-multiple-instances", "Help Menu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool startInTray = args.Contains("-t") || args.Contains("--start-in-tray");
            bool allowMultipleInstances = args.Contains("-m") || args.Contains("--allow-multiple-instances");

            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()!.Location)).Length > 1)
            {
                if (!allowMultipleInstances)
                {
                    MessageBox.Show("Another instance is already running.\n\nUse -m or --allow-multiple-instances if you wish to start a second duplicate instance.\n\nThis instance will now close.", "Multiple Instances", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(NotificationListener, startInTray));
        }
    }
}
