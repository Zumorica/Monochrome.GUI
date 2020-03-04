using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monochrome.GUI.Controls
{
    public class TextureButton : BaseButton
    {
        private Vector2 _scale = new Vector2(1, 1);
        private Texture2D _textureNormal;
        public const string StylePropertyTexture = "texture";
        public const string StylePseudoClassNormal = "normal";
        public const string StylePseudoClassHover = "hover";
        public const string StylePseudoClassDisabled = "disabled";
        public const string StylePseudoClassPressed = "pressed";

        public TextureButton()
        {
            DrawModeChanged();
        }
        
        public Texture2D TextureNormal
        {
            get => _textureNormal;
            set
            {
                _textureNormal = value;
                MinimumSizeChanged();
            }
        }

        public Vector2 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                MinimumSizeChanged();
            }
        }

        protected override void DrawModeChanged()
        {
            switch (DrawMode)
            {
                case DrawModeEnum.Normal:
                    SetOnlyStylePseudoClass(StylePseudoClassNormal);
                    break;
                case DrawModeEnum.Pressed:
                    SetOnlyStylePseudoClass(StylePseudoClassPressed);
                    break;
                case DrawModeEnum.Hover:
                    SetOnlyStylePseudoClass(StylePseudoClassHover);
                    break;
                case DrawModeEnum.Disabled:
                    SetOnlyStylePseudoClass(StylePseudoClassDisabled);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected internal override void Draw(DrawingHandleScreen handle)
        {
            var texture = TextureNormal;

            if (texture == null)
            {
                TryGetStyleProperty(StylePropertyTexture, out texture);
                if (texture == null)
                {
                    return;
                }
            }

            handle.DrawTextureRectRegion(texture, PixelSizeBox);
        }

        protected override Vector2 CalculateMinimumSize()
        {
            var texture = TextureNormal;

            if (texture == null)
            {
                TryGetStyleProperty(StylePropertyTexture, out texture);
            }

            return Scale * (texture?.Size() ?? Vector2.Zero);
        }
    }
}
