using glib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MassGame
{
    /// <summary>
    /// Herní svět / mapa.
    /// </summary>
    class World : IUpdate, IDraw
    {
        private GlibWindow window;
        private Block[][] blocks;
        private Player player;

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

            player = new Player(window, window.Content.Load<Texture2D>("blocks/block_player"));
            player.Origin = player.Center;
            player.Position += player.Center + new Vector2(player.Width * 3f, player.Height * 3f);

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

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
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
                for (int y = 0; y < Height; y++)
                {
                    Block block = blocks[x][y];
                    block.Draw(sprite, gameTime);
                    sprite.DrawString(((MassGame)window).Font, x + "," + y, block.Position + new Vector2(5, 5), Color.Silver);

                    if (block.Contains(player.Position - new Vector2(block.Width, block.Height) + player.Center * 2f))
                    {
                        if (player.CurrentBlock == null || !player.CurrentBlock.Equals(block))
                        {
                            player.CurrentBlock = block;
                            System.Console.WriteLine("Current block: " + block.Position + ", Type: " + System.Enum.GetName(typeof(BlockType), block.Type));
                        }
                    }

                    if (player.Contains(block.Width * Width - 2, (int)player.Y))
                    {
                        /*
                        Block[] newVector = new Block[Height + 1];

                        for (int z = 0; z < newVector.Length; z++)
                        {
                            newVector[z] = new Block(blocks[x][y]);
                        }

                        Array.Resize<Block>(ref blocks, newVector.Length);
                        Array.Copy(newVector, blocks, newVector.Length);
                        Height = newVector.Length;
                        */
                    }
                }
            }
            sprite.Draw(player.CurrentBlock.Texture, player.CurrentBlock.Position, Color.Red);

            player.Draw(sprite, gameTime);
        }

        /// <summary>
        /// Vytvoří / vygeneruje svět.
        /// </summary>
        private void CreateWorld()
        {
            blocks = new Block[Width][];

            Sprite stone = new Sprite(window, window.Content.Load<Texture2D>("blocks/block_stone"));
            Sprite wall = new Sprite(window, window.Content.Load<Texture2D>("blocks/block_wall"));

            for (int x = 0; x < Width; x++)
            {
                blocks[x] = new Block[Height];

                for (int y = 0; y < Width; y++)
                {
                    Block block;

                    if (y == 0 || y == Width - 1)
                    {
                        block = new Block(wall);
                        block.Type = BlockType.Wall;
                    }
                    else
                    {
                        block = new Block(stone);
                        block.Type = BlockType.Stone;
                    }
                    block.Position = new Vector2(x * block.Width * block.Scale.X, y * block.Height * block.Scale.Y);
                    blocks[x][y] = block;
                }
            }
        }
    }
}
