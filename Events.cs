using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monochrome.GUI
{
    public abstract class InputEventArgs : EventArgs
    {
        public bool Handled { get; private set; }
        
        /// <summary>
        ///     Whether the Bound key can change the focused control.
        /// </summary>
        public bool CanFocus { get; internal set; }

        /// <summary>
        ///     Mark this event as handled.
        /// </summary>
        public void Handle()
        {
            Handled = true;
        }
    }

    /// <summary>
    ///     Generic input event that has modifier keys like control.
    /// </summary>
    public abstract class ModifierInputEventArgs : InputEventArgs
    {
        /// <summary>
        ///     Whether the alt key (⌥ Option on MacOS) is held.
        /// </summary>
        public bool Alt { get; }

        /// <summary>
        ///     Whether the control key is held.
        /// </summary>
        public bool Control { get; }

        /// <summary>
        ///     Whether the shift key is held.
        /// </summary>
        public bool Shift { get; }

        /// <summary>
        ///     Whether the system key (Windows key, ⌘ Command on MacOS) is held.
        /// </summary>
        public bool System { get; }

        protected ModifierInputEventArgs(bool alt, bool control, bool shift, bool system)
        {
            Alt = alt;
            Control = control;
            Shift = shift;
            System = system;
        }
    }

    public class TextEventArgs : InputEventArgs
    {
        public TextEventArgs(uint codePoint)
        {
            CodePoint = codePoint;
        }

        public uint CodePoint { get; }
    }

    public class KeyEventArgs : ModifierInputEventArgs
    {
        /// <summary>
        ///     The key that got pressed or released.
        /// </summary>
        public Keys Key { get; }

        /// <summary>
        ///     If true, this key is being held down and another key event is being fired by the OS.
        /// </summary>
        public bool IsRepeat { get; }
        
        public Vector2 PointerLocation { get; }
        
        public KeyState State { get; }

        public KeyEventArgs(Keys key, KeyState state, Vector2 pointerLocation, bool repeat, bool alt, bool control, bool shift, bool system)
            : base(alt, control, shift, system)
        {
            Key = key;
            State = state;
            PointerLocation = pointerLocation;
            IsRepeat = repeat;
        }
    }

    public abstract class MouseEventArgs : InputEventArgs
    {
        /// <summary>
        ///     Position of the mouse relative to the screen.
        /// </summary>
        public Vector2 Position { get; }

        public Vector2 PointerLocation => Position;

        protected MouseEventArgs(Vector2 position)
        {
            Position = position;
        }
    }

    public class MouseButtonEventArgs : MouseEventArgs
    {
        /// <summary>
        ///     The mouse button that has been pressed or released.
        /// </summary>
        public MouseButton Button { get; }
        
        public KeyState State { get; }

        // ALL the parameters!
        public MouseButtonEventArgs(MouseButton button, KeyState state, Vector2 position)
            : base(position)
        {
            Button = button;
            State = state;
            CanFocus = true;
        }
    }

    public class MouseWheelEventArgs : MouseEventArgs
    {
        /// <summary>
        ///     The direction the mouse wheel was moved in.
        /// </summary>
        public Vector2 Delta { get; }

        // ALL the parameters!
        public MouseWheelEventArgs(Vector2 delta, Vector2 position)
            : base(position)
        {
            Delta = delta;
        }
    }

    public class MouseMoveEventArgs : MouseEventArgs
    {
        /// <summary>
        ///     The new position relative to the previous position.
        /// </summary>
        public Vector2 Relative { get; }

        // ALL the parameters!
        public MouseMoveEventArgs(Vector2 relative, Vector2 position)
            : base(position)
        {
            Relative = relative;
        }
    }
}
