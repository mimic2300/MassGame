using glib;
using Microsoft.Xna.Framework.Graphics;

namespace MassGame
{
    /// <summary>
    /// Kostka na mapě.
    /// </summary>
    class Block : Sprite
    {
        private BlockType type = BlockType.Empty;

        /// <summary>
        /// Hlavní konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        /// <param name="texture">Textůra kostky.</param>
        public Block(GlibWindow window, Texture2D texture)
            : base(window, texture)
        {
        }

        /// <summary>
        /// Konstruktor spritu kostky.
        /// </summary>
        /// <param name="sprite">Sprite kostky.</param>
        public Block(Sprite sprite)
            : base(sprite)
        {
        }

        /// <summary>
        /// Získá nebo nastaví typ kostky.
        /// </summary>
        public BlockType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
