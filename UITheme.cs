using Microsoft.Xna.Framework.Graphics;

namespace Monochrome.GUI
{
    // DON'T USE THESE
    // THEY'RE A BAD IDEA THAT NEEDS TO BE BURIED.

    /// <summary>
    ///     Fallback theme system for GUI.
    /// </summary>
    public abstract class UITheme
    {
        public abstract SpriteFont DefaultFont { get; }
        public abstract SpriteFont LabelFont { get; }
        public abstract StyleBox PanelPanel { get; }
        public abstract StyleBox ButtonStyle { get; }
        public abstract StyleBox LineEditBox { get; }
    }

    public sealed class UIThemeDummy : UITheme
    {
        public override SpriteFont DefaultFont { get; } = null;
        public override SpriteFont LabelFont { get; } = null;
        public override StyleBox PanelPanel { get; } = new StyleBoxFlat();
        public override StyleBox ButtonStyle { get; } = new StyleBoxFlat();
        public override StyleBox LineEditBox { get; } = new StyleBoxFlat();
    }
}
