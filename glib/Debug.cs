using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace glib
{
    /// <summary>
    /// Debug hlavního okna.
    /// </summary>
    public class Debug : IDraw
    {
        private readonly GlibWindow window;
        private Texture2D lineTexture;

        #region Konstruktory

        /// <summary>
        /// Hlavní konstruktor.
        /// </summary>
        /// <param name="window"></param>
        public Debug(GlibWindow window)
        {
            this.window = window;
            IsVisible = true;
            LineSpace = 20;
            TextColor = Color.LimeGreen;
            lineTexture = Glib.CreateBlankTexture(window.GraphicsDevice);
        }

        #endregion Konstruktory

        #region Vlastnosti

        /// <summary>
        /// Získá nebo nastaví viditelnost debug výpisu.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Získá nebo nastaví font textu.
        /// </summary>
        public SpriteFont Font { get; set; }

        /// <summary>
        /// Získá nebo nastaví barvu textu.
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Získá nebo nastaví odsazení řádků textu.
        /// </summary>
        public int LineSpace { get; set; }

        #endregion Vlastnosti

        /// <summary>
        /// Vykreslí debug výpis.
        /// </summary>
        /// <param name="sprite">Sprite pro vykreslení.</param>
        /// <param name="gameTime">Herní čas.</param>
        public void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            if (IsVisible)
            {
                int x = 5;
                int y = 5;

                // vykreslí informace v horním levém rohu
                sprite.DrawString(Font, "[ DEBUG MODE: ENABLED ]", new Vector2(x, y), Color.Crimson);
                sprite.DrawString(Font, "FPS: " + window.FPS, new Vector2(x, y += LineSpace), TextColor);
                sprite.DrawString(Font, "Delta time: " + gameTime.ElapsedGameTime.TotalMilliseconds, new Vector2(x, y += LineSpace), TextColor);
                sprite.DrawString(Font, "Mouse: " + window.MouseState.ToString() + " | Is mouse in window: " + window.IsMouseInWindow, new Vector2(x, y += LineSpace), TextColor);
                sprite.DrawString(Font, "Fixed time step: " + window.IsFixedTimeStep, new Vector2(x, y += LineSpace), TextColor);
                sprite.DrawString(Font, "Fullscreen: " + window.IsFullscreen, new Vector2(x, y += LineSpace), TextColor);
                sprite.DrawString(Font, "Antialiasing: " + window.IsAntialiasing + " " + window.GraphicsDevice.PresentationParameters.MultiSampleCount + "x", new Vector2(x, y += LineSpace), TextColor);
                sprite.DrawString(Font, "V-Sync: " + window.IsVSync, new Vector2(x, y += LineSpace), TextColor);
                sprite.DrawString(Font, "Window frame: " + window.IsWindowFrame, new Vector2(x, y += LineSpace), TextColor);
                sprite.DrawString(Font, "Mouse visible: " + window.IsMouseVisible, new Vector2(x, y += LineSpace), TextColor);

                // vykreslí čáry pozice myši
                sprite.DrawLine(lineTexture, 1f, Color.Gray, window.MouseState.X, 0f, window.MouseState.X, window.Height);
                sprite.DrawLine(lineTexture, 1f, Color.Gray, 0f, window.MouseState.Y, window.Width, window.MouseState.Y);
            }
        }
    }
}
