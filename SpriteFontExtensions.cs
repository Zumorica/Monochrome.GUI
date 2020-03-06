using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monochrome.GUI
{
    public struct Metrics
    {
        public float Advance { get; internal set; }
        public float Width { get; internal set; }
        public float BearingX { get; internal set; }
        public float BearingY { get; internal set; }
    }
    
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

        public static bool TryGetCharMetrics(this SpriteFont font, char character, float uiScale, out Metrics metrics)
        {
            try
            {
                metrics = font.GetCharMetrics(character, uiScale);
                return true;
            } 
            catch
            {
                metrics = default;
                return false;
            }
        }
        
        public static Metrics GetCharMetrics(this SpriteFont font, char character, float uiScale)
        {
            var metrics = new Metrics();
            var glyph = font.GetGlyphs()[character];
            metrics.Width = glyph.Width;
            metrics.Advance = glyph.WidthIncludingBearings;
            metrics.BearingX = glyph.LeftSideBearing;
            metrics.BearingY = glyph.RightSideBearing;
            return metrics;
        }

        public static float GetAscent(this SpriteFont font, float uiScale)
        {
            return 0f;
        }

        internal static float DrawChar(this SpriteFont font, DrawingHandleScreen handle, char character, Vector2 baseLine, float uiScale, Color color)
        {
            return 0f;
        }
    }
}