using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monochrome.GUI
{
    public class SpriteBatchDrawingHandleScreen : DrawingHandleScreen
    {
        private SpriteBatch _spriteBatch;

        public SpriteBatchDrawingHandleScreen(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public override void DrawCircle(Vector2 position, float radius, Color color)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawLine(Vector2 point1, Vector2 point2, Color color, int thickness = 1)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            var texture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData(new[] {color});
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(distance, thickness);
            _spriteBatch.Draw(texture, point1, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }

        public override void DrawRect(Rectangle rect, Color color, bool filled = true, int borderWidth = 1)
        {
            rect.Location = Position.ToPoint();
            rect.Size = (rect.Size.ToVector2() * Scale).ToPoint();
            if (filled)
            {
                var texture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
                texture.SetData(new[] {color});
                _spriteBatch.Draw(texture, rect, color);
            }
            else
            {
                var texture = new Texture2D(_spriteBatch.GraphicsDevice, rect.Size.X, rect.Size.Y);
                var colors = new Color[ texture.Width * texture.Height ];
                for ( int x = 0; x < texture.Width; x++ ) {
                    for ( int y = 0; y < texture.Height; y++ ) {
                        bool colored = false;
                        for ( int i = 0; i <= borderWidth; i++ ) {
                            if ( x == i || y == i || x == texture.Width - 1 - i || y == texture.Height - 1 - i ) {
                                colors[x + y * texture.Width] = color;
                                colored = true;
                                break;
                            }
                        }

                        if(colored == false)
                            colors[ x + y * texture.Width ] = Color.Transparent;
                    }
                }

                texture.SetData( colors );
                _spriteBatch.Draw(texture, rect, color);
            }
        }

        public override void DrawText(SpriteFont font, string text, Vector2 position, Color? color = null)
        {
            _spriteBatch.DrawString(font, text, position, color ?? Color.White);
        }

        public override void DrawTextureRectRegion(Texture2D texture, UIBox2 rect, UIBox2? subRegion = null, Color? modulate = null)
        {
            _spriteBatch.Draw(texture, rect, subRegion, modulate ?? Color.White);
        }
    }
}