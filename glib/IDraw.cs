using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace glib
{
    /// <summary>
    /// Rozhraní s vykreslením objektu.
    /// </summary>
    public interface IDraw
    {
        /// <summary>
        /// Vykreslení objektu.
        /// </summary>
        /// <param name="sprite">Sprite pro vykreslení objektu.</param>
        /// <param name="gameTime">Herní čas.</param>
        void Draw(SpriteBatch sprite, GameTime gameTime);
    }
}
