using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IsometricGame
{
    public class IsometricGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch sprite;
        private Map map;

        public IsometricGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sprite = new SpriteBatch(GraphicsDevice);
            map = new Map(Content, 10, 10);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            map.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sprite.Begin();
            map.Draw(sprite);
            sprite.End();
            
            base.Draw(gameTime);
        }
    }
}
