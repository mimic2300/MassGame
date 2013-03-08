using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace IsometricGame
{
    class Map
    {
        public const int WIDTH = 128;
        public const int HEIGHT = 64;

        private List<Texture2D> textures;
        private Block[,] blocks;
        private int width;
        private int height;
        private Point offset;

        private SpriteFont font;

        public Map(ContentManager content, int width, int height)
        {
            offset = Point.Zero;

            this.width = width;
            this.height = height;

            font = content.Load<SpriteFont>("DebugFont");

            textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("RedBlock"));
            textures.Add(content.Load<Texture2D>("SelectedBlock"));

            blocks = new Block[width, height];

            Rectangle position = new Rectangle(0, 0, textures[0].Width, textures[0].Height);

            for (int x = 0; x < width; x++)
            {
                position.X = (WIDTH / 2) * x;
                position.Y = (HEIGHT / 2) * x;

                for (int y = 0; y < height; y++)
                {
                    blocks[x, y] = new Block(0, position);
                    position.X += (WIDTH / 2);
                    position.Y -= (HEIGHT / 2);
                }
            }
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            const int speed = 15;

            if (state.IsKeyDown(Keys.W))
                offset.Y += speed;

            if (state.IsKeyDown(Keys.S))
                offset.Y -= speed;

            if (state.IsKeyDown(Keys.A))
                offset.X += speed;

            if (state.IsKeyDown(Keys.D))
                offset.X -= speed;
        }

        public void Draw(SpriteBatch sprite)
        {
            Point mousePosition = new Point(Mouse.GetState().X, Mouse.GetState().Y);
            Rectangle drawPosition = Rectangle.Empty;

            for (int x = 0; x < width; x++)
            {
                for (int y = height - 1; y > -1; y--)
                {
                    Block block = blocks[x, y];
                    drawPosition = block.Rectangle;
                    drawPosition.X += offset.X;
                    drawPosition.Y += offset.Y;
                    
                    if (drawPosition.Contains(mousePosition))
                    {
                        sprite.Draw(textures[block.Tile + 1], drawPosition, Color.White);
                        sprite.DrawString(font,
                            new Vector2(drawPosition.X, drawPosition.Y).ToString(),
                            new Vector2(drawPosition.X, drawPosition.Y), Color.Blue);
                    }
                    else
                    {
                        sprite.Draw(textures[block.Tile], drawPosition, Color.White);
                    }
                }
            }
        }
    }
}
