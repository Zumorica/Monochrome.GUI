using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Monochrome.GUI.Controls
{
    public class HistoryLineEdit : LineEdit
    {
        private const int MaxHistorySize = 100;
        private string _historyTemp;

        public Keys TextHistoryPrev { get; set; } = Keys.Up;
        public Keys TextHistoryNext { get; set; } = Keys.Up;

        public List<string> History { get; } = new List<string>();
        public int HistoryIndex { get; set; } = 0;

        public event Action OnHistoryChanged;

        public HistoryLineEdit()
        {
            OnTextEntered += OnSubmit;
        }

        public void ClearHistory()
        {
            History.Clear();
            HistoryIndex = 0;
        }

        private void OnSubmit(LineEditEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Text))
            {
                return;
            }

            if (History.Count == 0 || History[History.Count - 1] != args.Text)
            {
                History.Add(args.Text);
                if (History.Count > MaxHistorySize)
                {
                    History.RemoveAt(0);
                }
            }
            HistoryIndex = History.Count;
            OnHistoryChanged?.Invoke();
        }

        protected internal override void KeyDown(GUIKeyEventArgs args)
        {
            base.KeyDown(args);

            if (!HasKeyboardFocus())
            {
                return;
            }

            if (args.Key == TextHistoryPrev)
            {
                if (HistoryIndex <= 0)
                {
                    return;
                }

                if (HistoryIndex == History.Count)
                {
                    _historyTemp = Text;
                }

                HistoryIndex--;
                Text = History[HistoryIndex];
                CursorPos = Text.Length;

                args.Handle();
            }
            else if (args.Key == TextHistoryNext)
            {
                if (HistoryIndex >= History.Count)
                {
                    return;
                }

                HistoryIndex++;

                Text = HistoryIndex == History.Count ? _historyTemp : History[HistoryIndex];

                CursorPos = Text.Length;

                args.Handle();
            }
        }
    }
}
