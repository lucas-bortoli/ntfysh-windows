using System.Drawing;

namespace ntfysh_client.Themes
{
    internal class DarkModeTheme: BaseTheme
    {
        public override Color BackgroundColor { get => SystemColors.ControlDark; }
        public override Color ControlBackGroundColor { get => Color.Black; }
        public override Color ControlMouseOverBackgroundColor { get => Color.Silver; }
        public override Color ForegroundColor { get => SystemColors.WindowText; }
    }
}
