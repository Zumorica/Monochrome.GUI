using Microsoft.Xna.Framework;

namespace Monochrome.GUI.Controls
{
    public class PanelContainer : Container
    {
        public const string StylePropertyPanel = "panel";

        private StyleBox _panelOverride;

        public StyleBox PanelOverride
        {
            get => _panelOverride;
            set => _panelOverride = value;
        }

        protected internal override void Draw(DrawingHandleScreen handle)
        {
            base.Draw(handle);

            var style = _getStyleBox();
            style?.Draw(handle, PixelSizeBox);
        }

        protected override void LayoutUpdateOverride()
        {
            var contentBox = _getStyleBox()?.GetContentBox(PixelSizeBox) ?? SizeBox;

            foreach (var child in Children)
            {
                FitChildInPixelBox(child, (UIBox2i) contentBox);
            }
        }

        protected override Vector2 CalculateMinimumSize()
        {
            var styleSize = _getStyleBox()?.MinimumSize ?? Vector2.Zero;
            var childSize = Vector2.Zero;
            foreach (var child in Children)
            {
                childSize = Vector2Helper.ComponentMax(childSize, child.CombinedMinimumSize);
            }

            return styleSize / UIScale + childSize;
        }

        [System.Diagnostics.Contracts.Pure]
        private StyleBox _getStyleBox()
        {
            if (_panelOverride != null)
            {
                return _panelOverride;
            }

            TryGetStyleProperty(StylePropertyPanel, out StyleBox box);
            return box;
        }
    }
}
