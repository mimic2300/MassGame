using glib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MassGame
{
    public class MassGame : GlibWindow
    {
        private Debug debug;
        private World world;
        private Texture2D backgroundTexture;
        private SpriteFont font;

        #region Vlastnosti

        public SpriteFont Font
        {
            get { return font; }
        }

        #endregion Vlastnosti

        protected override void Initialize()
        {
            base.Initialize();

            Resolution = ResolutionType.R_720x480;

            font = Load<SpriteFont>("FontDebug");
            backgroundTexture = Load<Texture2D>("Background");

            debug = new Debug(this)
            {
                Font = font,
                IsVisible = false
            };

            world = new World(this, 7, 7);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // ukonèí hru
            if (KeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // zapne / vypne zobrazení debug výpisu
            if (KeyboardState.IsKeyDown(Keys.F3))
                debug.IsVisible = !debug.IsVisible;

            // zapne / vypne fullscreen
            if (KeyboardState.IsKeyDown(Keys.F))
                IsFullscreen = !IsFullscreen;

            // zapne / vypne rámeèek okna
            if (KeyboardState.IsKeyDown(Keys.W))
                IsWindowFrame = !IsWindowFrame;

            // zapne / vypne vyhlazení
            if (KeyboardState.IsKeyDown(Keys.A))
                IsAntialiasing = !IsAntialiasing;

            // zapne / vypne V-Sync
            if (KeyboardState.IsKeyDown(Keys.V))
                IsVSync = !IsVSync;

            // zapne / vypne fixní FPS
            if (KeyboardState.IsKeyDown(Keys.T))
                IsFixedTimeStep = !IsFixedTimeStep;

            // zapne / vypne zobrazení myši
            if (KeyboardState.IsKeyDown(Keys.M))
                IsMouseVisible = !IsMouseVisible;

            world.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Sprite.Begin();

            DrawBackground();

            world.Draw(Sprite, gameTime);
            debug.Draw(Sprite, gameTime);

            Sprite.End();
        }

        /// <summary>
        /// Vykreslí pozadí z patternu.
        /// </summary>
        private void DrawBackground()
        {
            for (int x = 0; x < Width; x += backgroundTexture.Width)
            {
                for (int y = 0; y < Height; y += backgroundTexture.Height)
                {
                    Sprite.Draw(backgroundTexture, new Vector2(x, y), Color.White);
                }
            }
        }
    }
}
