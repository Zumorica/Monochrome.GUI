using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monochrome.GUI
{
    public abstract class DrawingHandleScreen : DrawingHandleBase
    {
        public abstract void DrawRect(Rectangle rect, Color color, bool filled = true, int borderWidth = 1);
        public abstract void DrawText(SpriteFont font, string text, Vector2 position, Color? color = null);

        public abstract void DrawTextureRectRegion(Texture2D texture, UIBox2 rect, UIBox2? subRegion = null, Color? modulate = null);

        public void DrawTexture(Texture2D texture, Vector2 position, Color? modulate = null)
        {
            CheckDisposed();

            DrawTextureRect(texture, UIBox2.FromDimensions(position.X, position.Y, texture.Width, texture.Height), modulate);
        }

        public void DrawTextureRect(Texture2D texture, UIBox2 rect, Color? modulate = null)
        {
            CheckDisposed();

            DrawTextureRectRegion(texture, rect, null, modulate);
        }
    }
}
