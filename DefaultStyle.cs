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

            Stylesheet = new Stylesheet(new []
            {
                new StyleRule(new SelectorElement(typeof(ContainerButton), null, null, new[] {ContainerButton.StylePseudoClassNormal}), new[]
                {
                    new StyleProperty(ContainerButton.StylePropertyStyleBox, new StyleBoxFlat(){BackgroundColor = Color.DarkGray}),
                }),
                
                new StyleRule(new SelectorElement(typeof(ContainerButton), null, null, new[] {ContainerButton.StylePseudoClassHover}), new[]
                {
                    new StyleProperty(ContainerButton.StylePropertyStyleBox, new StyleBoxFlat(){BackgroundColor = Color.Gray}),
                }),
                
                new StyleRule(new SelectorElement(typeof(ContainerButton), null, null, new[] {ContainerButton.StylePseudoClassPressed}), new[]
                {
                    new StyleProperty(ContainerButton.StylePropertyStyleBox, new StyleBoxFlat(){BackgroundColor = Color.DimGray}),
                }),
            });
        }
    }
}