using glib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MassGame
{
    /// <summary>
    /// Herní svět / mapa.
    /// </summary>
    class World : IDraw
    {
        private GlibWindow window;
        private Block[,] blocks;

        #region Konstruktory

        /// <summary>
        /// Hlavní konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        /// <param name="width">Počet bloků na šířku.</param>
        /// <param name="height">Počet bloků na výšku.</param>
        public World(GlibWindow window, int width, int height)
        {
            this.window = window;
            Width = width;
            Height = height;

            CreateWorld();
        }

        #endregion Konstruktory

        #region Vlastnosti

        /// <summary>
        /// Šířka světa (počet bloků na šířku).
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Výška světa (počet bloků na výšku).
        /// </summary>
        public int Height { get; private set; }

        #endregion Vlastnosti

        /// <summary>
        /// Vytvoří / vygeneruje svět.
        /// </summary>
        private void CreateWorld()
        {
            blocks = new Block[Width, Height];

            Sprite sprite = new Sprite(window, window.Content.Load<Texture2D>("blocks/block_stone"));

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    Block block = new Block(sprite);
                    block.Type = BlockType.Stone;
                    block.SetScale(0.5f);
                    block.Position = new Vector2(x * block.Width * block.Scale.X, y * block.Height * block.Scale.Y);
                    blocks[x, y] = block;
                }
            }
        }

        /// <summary>
        /// Vykreslí svět.
        /// </summary>
        /// <param name="sprite">Sprite pro vykreslení.</param>
        /// <param name="gameTime">Herní čas.</param>
        public void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    Block block = blocks[x, y];
                    Vector2 tmp = blocks[x, y].Position;

                    block.Position += new Vector2(200, 400);

                    if (block.Contains(window.MouseState.X, window.MouseState.Y))
                    {
                        block.Color = new Color(120, 120, 120, 160);
                        block.Draw(sprite, gameTime);
                        sprite.DrawString(((MassGame)window).Font, ":-)", block.Position, Color.YellowGreen);
                    }
                    else
                    {
                        block.Draw(sprite, gameTime);
                    }
                    block.Position = tmp;
                    block.Color = Color.White;
                }
            }
        }
    }
}
