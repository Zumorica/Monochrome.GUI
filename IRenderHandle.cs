using Microsoft.Xna.Framework;

namespace Monochrome.GUI
{
    internal interface IRenderHandle
    {
        DrawingHandleScreen DrawingHandleScreen { get; }

        void SetScissor(UIBox2? scissorBox);
    }
}