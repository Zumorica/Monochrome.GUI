using Microsoft.Xna.Framework.Graphics;

namespace Monochrome.GUI
{
    public static class SpriteFontExtensions
    {
        public static int GetLineHeight(this SpriteFont font, float scale)
        {
            return (int) (font.LineSpacing * scale);
        }
        
        public static int GetHeight(this SpriteFont font, float scale)
        {
            return (int) (font.LineSpacing * scale);
        }
    }
}