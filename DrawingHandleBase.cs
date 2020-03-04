using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Monochrome.GUI
{
    /// <summary>
    ///     Used for doing direct drawing without sprite components, existing GUI controls, etc...
    /// </summary>
    public abstract class DrawingHandleBase : IDisposable
    {
        //private protected IRenderHandle _renderHandle;
        private protected readonly int _handleId;
        public bool Disposed { get; private set; }
        public Color Modulate { get; set; } = Color.White;
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        public void Dispose()
        {
            Disposed = true;
        }

        public void SetTransform(Vector2 position, Vector2 scale)
        {
            CheckDisposed();
            Position = position;
            Scale = scale;
        }

        [DebuggerStepThrough]
        protected void CheckDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(nameof(DrawingHandleBase));
            }
        }

        public abstract void DrawCircle(Vector2 position, float radius, Color color);

        public abstract void DrawLine(Vector2 from, Vector2 to, Color color, int thickness = 1);
    }
}
