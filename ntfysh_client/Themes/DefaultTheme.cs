using System.Drawing;

namespace ntfysh_client.Themes
{
    internal class DefaultTheme: BaseTheme
    {
        public override Color BackgroundColor { get => Color.White; }
        public override Color ControlBackGroundColor { get => SystemColors.ControlDark; }
        public override Color ControlMouseOverBackgroundColor { get => Color.CadetBlue; }
        public override Color ForegroundColor { get => SystemColors.WindowText; }
    }
}
