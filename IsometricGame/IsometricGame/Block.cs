using Microsoft.Xna.Framework;

namespace IsometricGame
{
    struct Block
    {
        private Vector2 position;
        public byte Tile;
        public Rectangle Rectangle;

        public Block(byte tile, Rectangle rectangle)
        {
            Tile = tile;
            Rectangle = rectangle;
            position = new Vector2(rectangle.X, rectangle.Y);
        }

        public Vector2 Position { get { return position; } }
    }
}
