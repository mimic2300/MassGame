using glib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MassGame
{
    public class MassGame : GlibWindow
    {
        private Debug debug;
        private Player player;
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

            Resolution = ResolutionType.R_1280x800;

            font = Load<SpriteFont>("FontDebug");
            backgroundTexture = Load<Texture2D>("Background");

            debug = new Debug(this)
            {
                Font = font
            };

            player = new Player(this, Load<Texture2D>("Head"));
            player.Origin = player.Center;
            player.Position = new Vector2(player.Width, 300);

            world = new World(this, 5, 5);
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

            player.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Sprite.Begin();
            DrawBackground();

            // pouze debug hráèe
            Sprite.DrawString(debug.Font, "Position: " + player.Position.ToString(),
                new Vector2(5 + player.X + player.Width / 2, 5 + player.Y - player.Height / 2), Color.Yellow);
            Sprite.DrawString(debug.Font, "Rotation: " + player.Rotation.ToString(),
                new Vector2(5 + player.X + player.Width / 2, 5 + player.Y - player.Height / 2 + 15), Color.Yellow);
            Sprite.DrawString(debug.Font, "Scale: " + player.Scale.ToString(),
                new Vector2(5 + player.X + player.Width / 2, 5 + player.Y - player.Height / 2 + 30), Color.Yellow);
            Sprite.DrawString(debug.Font, "Size: " + player.Width + "x" + player.Height,
                new Vector2(5 + player.X + player.Width / 2, 5 + player.Y - player.Height / 2 + 45), Color.Yellow);
            Sprite.DrawString(debug.Font, "Speed: " + player.Speed,
                new Vector2(5 + player.X + player.Width / 2, 5 + player.Y - player.Height / 2 + 60), Color.Yellow);
            Sprite.DrawString(debug.Font, "Controls: arrows and [space]",
                new Vector2(5 + player.X + player.Width / 2, 5 + player.Y - player.Height / 2 + 75), Color.LightSkyBlue);
            player.Draw(Sprite, gameTime);

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
