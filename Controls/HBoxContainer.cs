namespace Monochrome.GUI.Controls
{
    /// <summary>
    ///     Container that lays its children out horizontally: from left to right.
    /// </summary>
    public class HBoxContainer : BoxContainer
    {
        private protected override bool Vertical => false;
    }
}
