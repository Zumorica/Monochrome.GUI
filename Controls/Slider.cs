using Microsoft.Xna.Framework;
using static Monochrome.GUI.Controls.LayoutContainer;

namespace Monochrome.GUI.Controls
{
    public class Slider : Range
    {
        public const string StylePropertyBackground = "background";
        public const string StylePropertyForeground = "foreground";
        public const string StylePropertyFill = "fill";
        public const string StylePropertyGrabber = "grabber";

        private readonly PanelContainer _foregroundPanel;
        private readonly PanelContainer _backgroundPanel;
        private readonly PanelContainer _fillPanel;
        private readonly PanelContainer _grabber;

        private bool _grabbed;

        private StyleBox _backgroundStyleBoxOverride;
        private StyleBox _foregroundStyleBoxOverride;
        private StyleBox _fillStyleBoxOverride;
        private StyleBox _grabberStyleBoxOverride;

        public StyleBox ForegroundStyleBoxOverride
        {
            get => _foregroundStyleBoxOverride;
            set
            {
                _foregroundStyleBoxOverride = value;
                UpdateStyleBoxes();
            }
        }

        public StyleBox BackgroundStyleBoxOverride
        {
            get => _backgroundStyleBoxOverride;
            set
            {
                _backgroundStyleBoxOverride = value;
                UpdateStyleBoxes();
            }
        }

        public StyleBox FillStyleBoxOverride
        {
            get => _fillStyleBoxOverride;
            set
            {
                _fillStyleBoxOverride = value;
                UpdateStyleBoxes();
            }
        }

        public StyleBox GrabberStyleBoxOverride
        {
            get => _grabberStyleBoxOverride;
            set
            {
                _grabberStyleBoxOverride = value;
                UpdateStyleBoxes();
            }
        }

        public Slider()
        {
            AddChild(new LayoutContainer
            {
                MouseFilter = MouseFilterMode.Ignore,
                Children =
                {
                    (_backgroundPanel = new PanelContainer
                    {
                        MouseFilter = MouseFilterMode.Ignore,
                    }),
                    (_fillPanel = new PanelContainer
                    {
                        MouseFilter = MouseFilterMode.Ignore,
                    }),
                    (_foregroundPanel = new PanelContainer
                    {
                        MouseFilter = MouseFilterMode.Ignore,
                    }),
                    (_grabber = new PanelContainer
                    {
                        MouseFilter = MouseFilterMode.Ignore,
                    })
                }
            });

            SetAnchorBottom(_fillPanel, 1);
            SetAnchorBottom(_grabber, 1);
            SetGrowHorizontal(_grabber, GrowDirection.Both);
            SetGrowVertical(_grabber, GrowDirection.Both);
            SetAnchorPreset(_foregroundPanel, LayoutPreset.Wide);
            SetAnchorPreset(_backgroundPanel, LayoutPreset.Wide);
        }

        public override float Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                UpdateValue();
            }
        }

        private void UpdateValue()
        {
            var ratio = GetAsRatio();

            var margin = (Width - _grabber.CombinedMinimumSize.X) * ratio + _grabber.CombinedMinimumSize.X / 2;
            SetMarginRight(_fillPanel, margin);
            SetMarginLeft(_grabber, margin);
            SetMarginRight(_grabber, margin);
        }

        protected override void Resized()
        {
            base.Resized();

            UpdateValue();
        }

        protected internal override void KeyBindDown(GUIBoundKeyEventArgs args)
        {
            base.KeyBindDown(args);

            if (!args.CanFocus)
            {
                return;
            }

            HandlePositionChange(args.RelativePosition);
            _grabbed = true;
        }

        protected internal override void KeyBindUp(GUIBoundKeyEventArgs args)
        {
            base.KeyBindUp(args);

            if (args.CanFocus)
            {
                _grabbed = false;
            }
        }

        protected internal override void MouseMove(GUIMouseMoveEventArgs args)
        {
            if (!_grabbed)
            {
                return;
            }

            HandlePositionChange(args.RelativePosition);
        }

        private void HandlePositionChange(Vector2 position)
        {
            var grabberWidth = _grabber.CombinedMinimumSize.X;
            var ratio = (position.X - grabberWidth / 2) / (Width - grabberWidth);
            SetAsRatio(ratio);
        }

        protected override void StylePropertiesChanged()
        {
            base.StylePropertiesChanged();

            UpdateStyleBoxes();
        }

        private void UpdateStyleBoxes()
        {
            StyleBox GetStyleBox(string name)
            {
                if (TryGetStyleProperty(name, out StyleBox box))
                {
                    return box;
                }

                return null;
            }

            _backgroundPanel.PanelOverride = BackgroundStyleBoxOverride ?? GetStyleBox(StylePropertyBackground);
            _foregroundPanel.PanelOverride = BackgroundStyleBoxOverride ?? GetStyleBox(StylePropertyForeground);
            _fillPanel.PanelOverride = FillStyleBoxOverride ?? GetStyleBox(StylePropertyFill);
            _grabber.PanelOverride = GrabberStyleBoxOverride ?? GetStyleBox(StylePropertyGrabber);
        }
    }
}
