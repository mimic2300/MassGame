using Microsoft.Xna.Framework;

namespace glib
{
    /// <summary>
    /// Rozhraní s aktualizací objektu.
    /// </summary>
    public interface IUpdate
    {
        /// <summary>
        /// Aktualizace logiky objektu.
        /// </summary>
        /// <param name="gameTime">Herní čas.</param>
        void Update(GameTime gameTime);
    }
}
