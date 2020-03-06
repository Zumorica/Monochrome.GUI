using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monochrome.GUI;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Monochrome.GUI.Sample
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GUIGameComponent _guiGameComponent;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _guiGameComponent = new GUIGameComponent(this, _graphics.GraphicsDevice);
            Components.Add(_guiGameComponent);
            _guiGameComponent.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == 
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
                Exit();
            base.Update(gameTime);
            
            _guiGameComponent.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _guiGameComponent.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}