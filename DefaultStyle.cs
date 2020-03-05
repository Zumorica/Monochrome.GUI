using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monochrome.GUI.Controls;

namespace Monochrome.GUI
{
    public class DefaultStyle
    {
        public Stylesheet Stylesheet { get; }
        private readonly GraphicsDevice _graphicsDevice;
        
        public DefaultStyle(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            
            var texture = new Texture2D(_graphicsDevice, 1, 1);
            texture.SetData(new Color[]{Color.Brown});
            
            var buttonNormal = new StyleBoxTexture
            {
                Texture = texture,
                Modulate = Color.White
            };
            buttonNormal.SetPatchMargin(StyleBox.Margin.All, 10);
            buttonNormal.SetPadding(StyleBox.Margin.All, 1);
            buttonNormal.SetContentMarginOverride(StyleBox.Margin.Vertical, 2);
            buttonNormal.SetContentMarginOverride(StyleBox.Margin.Horizontal, 14);
            
            Stylesheet = new Stylesheet(new []
            {
                new StyleRule(new SelectorElement(typeof(ContainerButton), null, null, new[] {ContainerButton.StylePseudoClassNormal}), new[]
                {
                    new StyleProperty(ContainerButton.StylePropertyStyleBox, buttonNormal),
                }),
            });
        }
    }
}