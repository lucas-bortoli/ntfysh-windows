using System.Drawing;

namespace ntfysh_client.Themes
{
    internal abstract class BaseTheme
    {
        public abstract Color BackgroundColor {get; }
        public abstract Color ControlBackGroundColor {get; }
        public abstract Color ControlMouseOverBackgroundColor { get; }
        public abstract Color ForegroundColor { get; }
    }
}
