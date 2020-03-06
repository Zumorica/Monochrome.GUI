using System.Reflection.Emit;
using static Monochrome.GUI.Controls.Label;

namespace Monochrome.GUI.Controls
{
    /// <summary>
    ///     Most common button type that draws text in a fancy box.
    /// </summary>
    public class Button : ContainerButton
    {
        public const string StyleClassButton = "button";

        public Label Label { get; }

        public Button() : base()
        {
            Label = new Label
            {
                StyleClasses = { StyleClassButton }
            };
            AddChild(Label);
        }

        /// <summary>
        ///     How to align the text inside the button.
        /// </summary>
        public BoxContainer.AlignMode TextAlign { get => (BoxContainer.AlignMode) Label.Align; set => Label.Align = (AlignMode) value; }

        /// <summary>
        ///     If true, the button will allow shrinking and clip text
        ///     to prevent the text from going outside the bounds of the button.
        ///     If false, the minimum size will always fit the contained text.
        /// </summary>
        public bool ClipText { get => Label.ClipText; set => Label.ClipText = value; }

        /// <summary>
        ///     The text displayed by the button.
        /// </summary>
        public string Text { get => Label.Text; set => Label.Text = value; }
    }
}
