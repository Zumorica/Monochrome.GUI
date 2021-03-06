﻿using System;
using Microsoft.Xna.Framework;
using Monochrome.GUI.Controls;

namespace Monochrome.GUI
{
    public interface IUserInterfaceManager
    {
        UITheme ThemeDefaults { get; }
        Stylesheet Stylesheet { get; set; }

        Control KeyboardFocused { get; }

        LayoutContainer StateRoot { get; }

        LayoutContainer WindowRoot { get; }

        LayoutContainer PopupRoot { get; }

        PopupContainer ModalRoot { get; }

        Control CurrentlyHovered { get; }

        float UIScale { get; }

        /// <summary>
        ///     The "root" control to which all other controls are parented,
        ///     potentially indirectly.
        /// </summary>
        Control RootControl { get; }

        //TODO: void Popup(string contents, string title = "Alert!");

        Control MouseGetControl(Vector2 coordinates);

        /// <summary>
        ///     Give a control keyboard focus, releasing focus on the currently focused control (if any).
        /// </summary>
        /// <param name="control">The control to give keyboard focus to.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="control"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if <see cref="control"/> has <see cref="Control.CanKeyboardFocus"/> <c>false</c>.
        /// </exception>
        void GrabKeyboardFocus(Control control);

        /// <summary>
        ///     Release keyboard focus from the currently focused control, if any.
        /// </summary>
        void ReleaseKeyboardFocus();

        /// <summary>
        ///     Conditionally release keyboard focus if <see cref="ifControl"/> has keyboard focus.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <see cref="ifControl"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ReleaseKeyboardFocus()"/>
        void ReleaseKeyboardFocus(Control ifControl);
    }

    internal interface IUserInterfaceManagerInternal : IUserInterfaceManager
    {
        /// <summary>
        ///     Clears and disposes of all UI components.
        ///     Highly destructive!
        /// </summary>
        void DisposeAllComponents();

        void Initialize();
        void InitializeTesting();

        void Update(GameTime args);

        void FrameUpdate(GameTime args);

        void InputEvent(InputEventArgs args);
        void MouseButtonDown(MouseButtonEventArgs args);
        void MouseButtonUp(MouseButtonEventArgs args);
        void MouseMove(MouseMoveEventArgs mouseMoveEventArgs);
        void MouseWheel(MouseWheelEventArgs args);
        void TextEntered(TextEventArgs args);
        void KeyDown(KeyEventArgs args);
        void KeyUp(KeyEventArgs args);

        void ControlHidden(Control control);

        void ControlRemovedFromTree(Control control);

        void PushModal(Control modal);

        void RemoveModal(Control modal);

        void Render(IRenderHandle renderHandle);

        void QueueStyleUpdate(Control control);
        void QueueLayoutUpdate(Control control);
    }
}

