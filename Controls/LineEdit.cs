﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monochrome.GUI.Controls
{
    /// <summary>
    ///     Allows the user to input and modify a line of text.
    /// </summary>
    public class LineEdit : Control
    {
        public const string StylePropertyStyleBox = "stylebox";
        public const string StyleClassLineEditNotEditable = "notEditable";
        public const string StylePseudoClassPlaceholder = "placeholder";

        [NotNull] private string _text = "";
        private bool _editable = true;
        private string _placeHolder;
        private int _cursorPosition;
        private float _cursorBlinkTimer;
        private bool _cursorCurrentlyLit;
        private const float BlinkTime = 0.5f;

        private bool IsPlaceHolderVisible => string.IsNullOrEmpty(_text) && _placeHolder != null;

        public event Action<LineEditEventArgs> OnTextChanged;
        public event Action<LineEditEventArgs> OnTextEntered;

        /// <summary>
        ///     Determines whether the LineEdit text gets changed by the input text.
        /// </summary>
        public Func<string, bool> IsValid { get; set; }

        public AlignMode TextAlign { get; set; }

        public string Text
        {
            get => _text;
            set
            {
                if (value == null)
                {
                    value = "";
                }

                if (!SetText(value))
                {
                    return;
                }
                _cursorPosition = 0;
                _updatePseudoClass();
            }
        }

        public bool Editable
        {
            get => _editable;
            set
            {
                _editable = value;
                if (!_editable)
                {
                    AddStyleClass(StyleClassLineEditNotEditable);
                }
                else
                {
                    RemoveStyleClass(StyleClassLineEditNotEditable);
                }
            }
        }

        public string PlaceHolder
        {
            get => _placeHolder;
            set
            {
                _placeHolder = value;
                _updatePseudoClass();
            }
        }

        public int CursorPos
        {
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > _text.Length)
                {
                    value = _text.Length;
                }
                _cursorPosition = value;
            }
        }

        public bool IgnoreNext { get; set; }

        // TODO:
        // I decided to not implement the entire LineEdit API yet,
        // since most of it won't be used yet (if at all).
        // Feel free to implement wrappers for all the other properties!
        // Future me reporting, thanks past me.
        // Second future me reporting, thanks again.
        // Third future me is here to say thanks.
        // Fourth future me is here to continue the tradition.

        public LineEdit()
        {
            MouseFilter = MouseFilterMode.Stop;
            CanKeyboardFocus = true;
            KeyboardFocusOnClick = true;
        }

        public void Clear()
        {
            Text = "";
        }

        public void Select(int from = 0, int to = -1)
        {
        }

        public void SelectAll()
        {
        }

        public void InsertAtCursor(string text)
        {
            // Strip newlines.
            var chars = new List<char>(text.Length);
            foreach (var chr in text)
            {
                if (chr == '\n')
                {
                    continue;
                }

                chars.Add(chr);
            }

            if (chars.Count == 0)
            {
                return;
            }

            if (!SetText(_text.Insert(_cursorPosition, new string(chars.ToArray()))))
            {
                return;
            }
            _cursorPosition += chars.Count;
            OnTextChanged?.Invoke(new LineEditEventArgs(this, _text));
            _updatePseudoClass();
        }

        protected bool SetText(string newText)
        {
            if (IsValid != null && !IsValid(newText))
            {
                return false;
            }

            _text = newText;
            return true;
        }

        protected internal override void Draw(DrawingHandleScreen handle)
        {
            var styleBox = _getStyleBox();
            var drawBox = PixelSizeBox;
            var contentBox = styleBox.GetContentBox(drawBox);
            styleBox.Draw(handle, drawBox);
            var font = _getFont();
            var renderedTextColor = _getFontColor();

            var offsetY = (int) (contentBox.Height - font.GetHeight(UIScale)) / 2;
            var baseLine = new Vector2(0, offsetY + font.GetAscent(UIScale)) + contentBox.TopLeft;

            string renderedText;

            if (IsPlaceHolderVisible)
            {
                renderedText = _placeHolder;
            }
            else
            {
                renderedText = _text;
            }

            float? actualCursorPosition = null;

            if (_cursorPosition == 0)
            {
                actualCursorPosition = contentBox.Left;
            }

            var count = 0;
            foreach (var chr in renderedText)
            {
                if (!font.TryGetCharMetrics(chr, UIScale, out var metrics))
                {
                    count += 1;
                    continue;
                }

                // Glyph would be outside the bounding box, abort.
                if (baseLine.X + metrics.Width + metrics.BearingX > contentBox.Right)
                {
                    break;
                }

                font.DrawChar(handle, chr, baseLine, UIScale, renderedTextColor);
                baseLine += new Vector2(metrics.Advance, 0);
                count += 1;
                if (count == _cursorPosition)
                {
                    actualCursorPosition = baseLine.X;
                }
            }

            if (_cursorCurrentlyLit && actualCursorPosition.HasValue && HasKeyboardFocus())
            {
                handle.DrawRect(
                    new UIBox2(actualCursorPosition.Value, contentBox.Top, actualCursorPosition.Value + 1,
                        contentBox.Bottom), Color.White);
            }
        }

        protected override void FrameUpdate(GameTime args)
        {
            base.FrameUpdate(args);

            _cursorBlinkTimer -= (float)args.ElapsedGameTime.TotalSeconds;
            if (_cursorBlinkTimer <= 0)
            {
                _cursorBlinkTimer += BlinkTime;
                _cursorCurrentlyLit = !_cursorCurrentlyLit;
            }
        }

        protected override Vector2 CalculateMinimumSize()
        {
            var font = _getFont();
            var style = _getStyleBox();
            return new Vector2(0, font.GetHeight(UIScale) / UIScale) + style.MinimumSize / UIScale;
        }

        protected internal override void TextEntered(GUITextEventArgs args)
        {
            base.TextEntered(args);

            if (!Editable)
            {
                return;
            }

            if (IgnoreNext)
            {
                IgnoreNext = false;
                return;
            }

            if (!SetText(_text.Insert(_cursorPosition, ((char)args.CodePoint).ToString())))
            {
                return;
            }
            _cursorPosition += 1;
            OnTextChanged?.Invoke(new LineEditEventArgs(this, _text));
            _updatePseudoClass();
        }

        public Keys TextBackspace { get; set; } = Keys.Back;
        public Keys TextCursorLeft { get; set; } = Keys.Left;
        public Keys TextCursorRight { get; set; } = Keys.Right;
        public Keys TextCursorBegin { get; set; } = Keys.Home;
        public Keys TextCursorEnd { get; set; } = Keys.End;
        public Keys TextSubmit { get; set; } = Keys.Enter;
        public Keys TextPaste { get; set; } = Keys.V;
        public Keys TextDelete { get; set; } = Keys.Delete;
        public Keys TextReleaseFocus { get; set; } = Keys.Escape;

        protected internal override void KeyDown(GUIKeyEventArgs args)
        {
            // TODO: Refactor to another thing
            base.KeyDown(args);
            
            if (!this.HasKeyboardFocus())
            {
                return;
            }
            if (args.Key == TextBackspace)
            {
                if (_cursorPosition == 0 || !Editable)
                {
                    return;
                }

                _text = _text.Remove(_cursorPosition - 1, 1);
                OnTextChanged?.Invoke(new LineEditEventArgs(this, _text));
                _cursorPosition -= 1;
                _updatePseudoClass();
                args.Handle();
            }
            else if (args.Key == TextCursorLeft)
            {
                if (_cursorPosition == 0)
                {
                    return;
                }

                _cursorPosition -= 1;
                args.Handle();
            }
            else if (args.Key == TextCursorRight)
            {
                if (_cursorPosition == _text.Length)
                {
                    return;
                }

                _cursorPosition += 1;
                args.Handle();
            }
            else if (args.Key == TextCursorBegin)
            {
                _cursorPosition = 0;
                args.Handle();
            }
            else if (args.Key == TextCursorEnd)
            {
                _cursorPosition = _text.Length;
                args.Handle();
            }
            else if (args.Key == TextSubmit)
            {
                if (!Editable)
                {
                    return;
                }

                OnTextEntered?.Invoke(new LineEditEventArgs(this, _text));
                args.Handle();
            }
            else if (args.Key == TextPaste)
            {
                if (!Editable)
                {
                    return;
                }

                //TODO: Clipboard....
                //InsertAtCursor(clipboard.GetText());
                args.Handle();
            }
            else if (args.Key == TextDelete)
            {
                if (_cursorPosition >= _text.Length || !Editable)
                {
                    return;
                }

                _text = _text.Remove(_cursorPosition, 1);
                OnTextChanged?.Invoke(new LineEditEventArgs(this, _text));
                _updatePseudoClass();
                args.Handle();
            }
            else if (args.Key == TextReleaseFocus)
            {
                ReleaseKeyboardFocus();
                args.Handle();
                return;
            }
            // Reset this so the cursor is always visible immediately after a keybind is pressed.
            _resetCursorBlink();
        }

        protected internal override void MouseButtonDown(GUIMouseButtonEventArgs args)
        {
            base.MouseButtonDown(args);
            
            // Find closest cursor position under mouse.
            var style = _getStyleBox();
            var contentBox = style.GetContentBox(PixelSizeBox);

            var clickPosX = args.RelativePosition.X * UIScale;

            var font = _getFont();
            var index = 0;
            var chrPosX = contentBox.Left;
            var lastChrPostX = contentBox.Left;
            foreach (var chr in _text)
            {
                if (!font.TryGetCharMetrics(chr, UIScale, out var metrics))
                {
                    index += 1;
                    continue;
                }

                if (chrPosX > clickPosX)
                {
                    break;
                }

                lastChrPostX = chrPosX;
                chrPosX += metrics.Advance;
                index += 1;

                if (chrPosX > contentBox.Right)
                {
                    break;
                }
            }

            // Distance between the right side of the glyph overlapping the mouse and the mouse.
            var distanceRight = chrPosX - clickPosX;
            // Same but left side.
            var distanceLeft = clickPosX - lastChrPostX;
            // If the mouse is closer to the left of the glyph we lower the index one, so we select before that glyph.
            if (index > 0 && distanceRight > distanceLeft)
            {
                index -= 1;
            }

            _cursorPosition = index;
            args.Handle();
        }

        protected internal override void FocusEntered()
        {
            base.FocusEntered();

            // Reset this so the cursor is always visible immediately after gaining focus..
            _resetCursorBlink();
        }

        [Pure]
        private SpriteFont _getFont()
        {
            if (TryGetStyleProperty("font", out SpriteFont font))
            {
                return font;
            }

            return UserInterfaceManager.ThemeDefaults.DefaultFont;
        }

        [Pure]
        private StyleBox _getStyleBox()
        {
            if (TryGetStyleProperty(StylePropertyStyleBox, out StyleBox box))
            {
                return box;
            }

            return UserInterfaceManager.ThemeDefaults.LineEditBox;
        }

        [Pure]
        private Color _getFontColor()
        {
            if (TryGetStyleProperty("font-color", out Color color))
            {
                return color;
            }

            return Color.White;
        }

        private void _updatePseudoClass()
        {
            SetOnlyStylePseudoClass(IsPlaceHolderVisible ? StylePseudoClassPlaceholder : null);
        }

        public enum AlignMode
        {
            Left = 0,
            Center = 1,
            Right = 2
        }

        public class LineEditEventArgs : EventArgs
        {
            public LineEdit Control { get; }
            public string Text { get; }

            public LineEditEventArgs(LineEdit control, string text)
            {
                Control = control;
                Text = text;
            }
        }

        private void _resetCursorBlink()
        {
            _cursorCurrentlyLit = true;
            _cursorBlinkTimer = BlinkTime;
        }
    }
}
