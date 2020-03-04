namespace Monochrome.GUI.Controls
{
    /// <summary>
    ///     Container that lays its children out vertically: from top to bottom.
    /// </summary>
    public class VBoxContainer : BoxContainer
    {
        private protected override bool Vertical => true;
    }
}
