using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace glib
{
    /// <summary>
    /// Globální a herní statické funkce nebo rožšiřující funkce.
    /// </summary>
    public static class Glib
    {
        /// <summary>
        /// Generátor náhodných hodnot.
        /// </summary>
        public static readonly Random Random = new Random(Environment.TickCount);

        /// <summary>
        /// vytvoří čistou textůru 1x1.
        /// </summary>
        /// <param name="graphics">Grafický ovladač.</param>
        /// <returns>Vrací čistou textůru.</returns>
        public static Texture2D CreateBlankTexture(GraphicsDevice graphics)
        {
            Texture2D blank = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
            return blank;
        }

        /// <summary>
        /// vykreslí čáru.
        /// </summary>
        /// <param name="sprite">Sprite pro vykreslení.</param>
        /// <param name="blankTexture">Textůra čáry (stačí čistá - blank).</param>
        /// <param name="width">Šířka čáry.</param>
        /// <param name="color">Barva.</param>
        /// <param name="begin">Startovní bod X a Y.</param>
        /// <param name="end">Konečný bod X a Y.</param>
        public static void DrawLine(this SpriteBatch sprite, Texture2D blankTexture, float width, Color color, Vector2 begin, Vector2 end)
        {
            float angle = (float)Math.Atan2(end.Y - begin.Y, end.X - begin.X);
            float length = Vector2.Distance(begin, end);

            sprite.Draw(blankTexture, begin, null, color, angle, Vector2.Zero, new Vector2(length, width), SpriteEffects.None, 0);
        }

        /// <summary>
        /// vykreslí čáru.
        /// </summary>
        /// <param name="sprite">Sprite pro vykreslení.</param>
        /// <param name="blankTexture">Textůra čáry (stačí čistá - blank).</param>
        /// <param name="width">Šířka čáry.</param>
        /// <param name="color">Barva.</param>
        /// <param name="begin">Startovní bod X a Y.</param>
        /// <param name="end">Konečný bod X a Y.</param>
        public static void DrawLine(this SpriteBatch sprite, Texture2D blankTexture,
            float width, Color color, float x1, float y1, float x2, float y2)
        {
            sprite.DrawLine(blankTexture, width, color, new Vector2(x1, y1), new Vector2(x2, y2));
        }
    }
}
