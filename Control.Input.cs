using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monochrome.GUI
{
    public partial class Control
    {
        /// <summary>
        ///     Invoked when the mouse enters the area of this control / when it hovers over the control.
        /// </summary>
        public event Action<GUIMouseHoverEventArgs> OnMouseEntered;

        protected internal virtual void MouseEntered()
        {
            OnMouseEntered?.Invoke(new GUIMouseHoverEventArgs(this));
        }

        /// <summary>
        ///     Invoked when the mouse exits the area of this control / when it stops hovering over the control.
        /// </summary>
        public event Action<GUIMouseHoverEventArgs> OnMouseExited;

        protected internal virtual void MouseExited()
        {
            OnMouseExited?.Invoke(new GUIMouseHoverEventArgs(this));
        }

        protected internal virtual void MouseWheel(GUIMouseWheelEventArgs args)
        {
        }

        public event Action<GUIMouseButtonEventArgs> OnMouseButtonDown;

        protected internal virtual void MouseButtonDown(GUIMouseButtonEventArgs args)
        {
            OnMouseButtonDown?.Invoke(args);
        }

        protected internal virtual void MouseButtonUp(GUIMouseButtonEventArgs args)
        {
        }

        protected internal virtual void MouseMove(GUIMouseMoveEventArgs args)
        {
        }

        protected internal virtual void KeyDown(GUIKeyEventArgs args)
        {
        }
        
        protected internal virtual void KeyUp(GUIKeyEventArgs args)
        {
        }

        protected internal virtual void TextEntered(GUITextEventArgs args)
        {
        }
    }

    public class GUIMouseHoverEventArgs : EventArgs
    {
        /// <summary>
        ///     The control this event originated from.
        /// </summary>
        public Control SourceControl { get; }

        public GUIMouseHoverEventArgs(Control sourceControl)
        {
            SourceControl = sourceControl;
        }
    }

    public class GUIMouseButtonEventArgs : GUIMouseEventArgs
    {
        /// <summary>
        ///     Position of the mouse, relative to the current control.
        /// </summary>
        public MouseButton Button { get; }
        public KeyState State { get; }

        public GUIMouseButtonEventArgs(MouseButton button, KeyState state, Control control, Vector2 globalPosition, Vector2 globalPixelPosition, Vector2 relativePosition, Vector2 relativePixelPosition) 
            : base(control, globalPosition, globalPixelPosition, relativePosition, relativePixelPosition)
        {
            Button = button;
            State = state;
            CanFocus = true;
        }
    }

    public class GUIKeyEventArgs : KeyEventArgs
    {
        /// <summary>
        ///     The control spawning this event.
        /// </summary>
        public Control SourceControl { get; }

        public GUIKeyEventArgs(Control sourceControl,
            Keys key,
            KeyState state,
            Vector2 pointerPosition,
            bool repeat,
            bool alt,
            bool control,
            bool shift,
            bool system)
            : base(key, state, pointerPosition, repeat, alt, control, shift, system)
        {
            SourceControl = sourceControl;
        }
    }

    public class GUITextEventArgs : TextEventArgs
    {
        /// <summary>
        ///     The control spawning this event.
        /// </summary>
        public Control SourceControl { get; }

        public GUITextEventArgs(Control sourceControl,
            uint codePoint)
            : base(codePoint)
        {
            SourceControl = sourceControl;
        }
    }

    public abstract class GUIMouseEventArgs : MouseEventArgs
    {
        /// <summary>
        ///     The control spawning this event.
        /// </summary>
        public Control SourceControl { get; internal set; }

        /// <summary>
        ///     Position of the mouse, relative to the screen.
        /// </summary>
        public Vector2 GlobalPosition { get; }

        public Vector2 GlobalPixelPosition { get; }

        /// <summary>
        ///     Position of the mouse, relative to the current control.
        /// </summary>
        public Vector2 RelativePosition { get; internal set; }

        public Vector2 RelativePixelPosition { get; internal set; }

        protected GUIMouseEventArgs(Control sourceControl,
            Vector2 globalPosition,
            Vector2 globalPixelPosition,
            Vector2 relativePosition,
            Vector2 relativePixelPosition) : base(globalPosition)
        {
            SourceControl = sourceControl;
            GlobalPosition = globalPosition;
            RelativePosition = relativePosition;
            RelativePixelPosition = relativePixelPosition;
            GlobalPixelPosition = globalPixelPosition;
        }
    }

    public class GUIMouseMoveEventArgs : GUIMouseEventArgs
    {
        /// <summary>
        ///     The new position relative to the previous position.
        /// </summary>
        public Vector2 Relative { get; }

        // ALL the parameters!
        public GUIMouseMoveEventArgs(Vector2 relative,
            Control sourceControl,
            Vector2 globalPosition,
            Vector2 globalPixelPosition,
            Vector2 relativePosition,
            Vector2 relativePixelPosition)
            : base(sourceControl, globalPosition, globalPixelPosition, relativePosition, relativePixelPosition)
        {
            Relative = relative;
        }
    }

    public class GUIMouseWheelEventArgs : GUIMouseEventArgs
    {
        public Vector2 Delta { get; }

        public GUIMouseWheelEventArgs(Vector2 delta,
            Control sourceControl,
            Vector2 globalPosition,
            Vector2 globalPixelPosition,
            Vector2 relativePosition,
            Vector2 relativePixelPosition)
            : base(sourceControl, globalPosition, globalPixelPosition, relativePosition, relativePixelPosition)
        {
            Delta = delta;
        }
    }
}