using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public static Vector2 ScreenSize
        {
            get
            {
                var d = Singleton._spriteBatch.GraphicsDevice.DisplayMode;
                return new Vector2(d.Width, d.Height);
            }
        }
        
        public delegate void OnWindowResized(Vector2 size);

        public GUIGameComponent(Game game, GraphicsDevice graphicsDevice) : base(game)
        {
            if(_singleton != null)
                throw new Exception("An instance of GUIGameComponent already exists!");

            Singleton = this;
            
            _game = game;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            DrawingHandleScreen = new SpriteBatchDrawingHandleScreen(_spriteBatch);
        }

        public override void Initialize()
        {
            base.Initialize();
            UserInterfaceManager.Initialize();
            var popup = new Popup() {CustomMinimumSize = new Vector2(300, 300)};
            
            UserInterfaceManager.RootControl.AddChild(popup);
            UserInterfaceManager.RootControl.AddChild(new ItemList(){CustomMinimumSize = new Vector2(300, 300)});
            
            popup.Open(new UIBox2(100, 20, 400, 600));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UserInterfaceManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;
            _spriteBatch.Begin();
            UserInterfaceManager.FrameUpdate(gameTime);
            UserInterfaceManager.Render(this);
            _spriteBatch.End();
        }
        
        public void SetScissor(UIBox2? scissorBox)
        {
            
        }
    }
}