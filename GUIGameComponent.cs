using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monochrome.GUI.Controls;

namespace Monochrome.GUI
{
    public class GUIGameComponent : DrawableGameComponent, IRenderHandle
    {
        private static GUIGameComponent _singleton;
        public static GUIGameComponent Singleton
        {
            get
            {
                if(_singleton == null)
                    throw new Exception("No instance of GUIGameComponent exists yet!");
                return _singleton;
            }
            private set => _singleton = value;
        }
        
        internal readonly IUserInterfaceManagerInternal UserInterfaceManager = new UserInterfaceManager();
        public DrawingHandleScreen DrawingHandleScreen { get; }
        private readonly SpriteBatch _spriteBatch;
        private readonly Game _game;
        private readonly DefaultStyle _style;
        private Rectangle? _scissorOriginal = null;
        private MouseState _oldMouseState;
        private KeyboardState _oldKeyboardState;
        private Point _screenSize;
        public static Vector2 ScreenSize => Singleton._screenSize.ToVector2();

        public event Action<Vector2> OnWindowResized;

        public GUIGameComponent(Game game, GraphicsDevice graphicsDevice) : base(game)
        {
            if(_singleton != null)
                throw new Exception("An instance of GUIGameComponent already exists!");

            Singleton = this;
            
            _game = game;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _style = new DefaultStyle(graphicsDevice);
            DrawingHandleScreen = new SpriteBatchDrawingHandleScreen(_spriteBatch);
            
        }

        public override void Initialize()
        {
            base.Initialize();
            UserInterfaceManager.Initialize();

            UserInterfaceManager.Stylesheet = _style.Stylesheet;

            var center = new CenterContainer() {MouseFilter = Control.MouseFilterMode.Pass};
            var item = new Button() { Text="Sample button!", CustomMinimumSize = new Vector2(100, 50) };
            
            center.AddChild(item);
            UserInterfaceManager.RootControl.AddChild(center);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateWindowSize();
            UpdateInput();
            UserInterfaceManager.Update(gameTime);
        }

        private void UpdateWindowSize()
        {
            if (_screenSize == Game.Window.ClientBounds.Size) return;
            _screenSize = Game.Window.ClientBounds.Size;
            OnWindowResized?.Invoke(_screenSize.ToVector2());
        }

        public void UpdateInput()
        {
            var newKeyboardState = Keyboard.GetState();
            var newMouseState = Mouse.GetState();

            foreach (var key in newKeyboardState.GetPressedKeys())
            {
                var @event = new KeyEventArgs(key, KeyState.Down, newMouseState.Position.ToVector2(), _oldKeyboardState.IsKeyDown(key), 
                    newKeyboardState.IsKeyDown(Keys.LeftAlt), newKeyboardState.IsKeyDown(Keys.LeftControl), 
                    newKeyboardState.IsKeyDown(Keys.LeftShift), newKeyboardState.IsKeyDown(Keys.LeftWindows));
                UserInterfaceManager.InputEvent(@event);
            }

            foreach (var key in _oldKeyboardState.GetPressedKeys())
            {
                if (!newKeyboardState.IsKeyUp(key)) continue;
                var @event = new KeyEventArgs(key, KeyState.Up, newMouseState.Position.ToVector2(), false, 
                    newKeyboardState.IsKeyDown(Keys.LeftAlt), newKeyboardState.IsKeyDown(Keys.LeftControl), 
                    newKeyboardState.IsKeyDown(Keys.LeftShift), newKeyboardState.IsKeyDown(Keys.LeftWindows));
                UserInterfaceManager.InputEvent(@event);
                UserInterfaceManager.InputEvent(@event);
            }

            if (newMouseState.LeftButton != _oldMouseState.LeftButton)
            {
                var @event = new MouseButtonEventArgs(MouseButton.Left, 
                    newMouseState.LeftButton == ButtonState.Pressed ? KeyState.Down : KeyState.Up, 
                    newMouseState.Position.ToVector2());
                UserInterfaceManager.InputEvent(@event);
            }
            
            if (newMouseState.RightButton != _oldMouseState.RightButton)
            {
                var @event = new MouseButtonEventArgs(MouseButton.Right, 
                    newMouseState.RightButton == ButtonState.Pressed ? KeyState.Down : KeyState.Up, 
                    newMouseState.Position.ToVector2());
                UserInterfaceManager.InputEvent(@event);
            }
            
            if (newMouseState.MiddleButton != _oldMouseState.MiddleButton)
            {
                var @event = new MouseButtonEventArgs(MouseButton.Middle, 
                    newMouseState.MiddleButton == ButtonState.Pressed ? KeyState.Down : KeyState.Up, 
                    newMouseState.Position.ToVector2());
                UserInterfaceManager.InputEvent(@event);
            }

            if (newMouseState.Position != _oldMouseState.Position)
            {
                var @event = new MouseMoveEventArgs((newMouseState.Position - _oldMouseState.Position).ToVector2(), newMouseState.Position.ToVector2());
                UserInterfaceManager.InputEvent(@event);
            }

            if (newMouseState.ScrollWheelValue != _oldMouseState.ScrollWheelValue
                || newMouseState.HorizontalScrollWheelValue != _oldMouseState.HorizontalScrollWheelValue)
            {
                var deltaY = newMouseState.ScrollWheelValue - _oldMouseState.ScrollWheelValue;
                var deltaX = newMouseState.HorizontalScrollWheelValue - _oldMouseState.HorizontalScrollWheelValue;
                var @event = new MouseWheelEventArgs(new Vector2(deltaX, deltaY), newMouseState.Position.ToVector2());
                UserInterfaceManager.InputEvent(@event);
            }

            _oldKeyboardState = newKeyboardState;
            _oldMouseState = newMouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;
            UserInterfaceManager.FrameUpdate(gameTime);
            _spriteBatch.Begin();
            UserInterfaceManager.Render(this);
            _spriteBatch.End();
        }
        
        public void SetScissor(UIBox2? scissorBox)
        {
            if (!scissorBox.HasValue)
            {
                if (!_scissorOriginal.HasValue)
                    return;
                _spriteBatch.GraphicsDevice.ScissorRectangle = _scissorOriginal.Value;
                return;
            }
            if (!_scissorOriginal.HasValue) _scissorOriginal = _spriteBatch.GraphicsDevice.ScissorRectangle;
            _spriteBatch.GraphicsDevice.ScissorRectangle = (Rectangle) scissorBox;
        }
    }
}