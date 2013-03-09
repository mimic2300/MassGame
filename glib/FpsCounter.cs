using Microsoft.Xna.Framework;
using System;

namespace glib
{
    /// <summary>
    /// Výpočet FPS.
    /// </summary>
    public class FpsCounter : IUpdate
    {
        private float currentFps = 0f;
        private float fpsCounter = 0f;
        private double elapsedTime = 0;

        /// <summary>
        /// Získá FPS.
        /// </summary>
        public int FPS { get { return (int)currentFps; } }

        /// <summary>
        /// Získá FPS v původním tvaru.
        /// </summary>
        public float FPSReal { get { return currentFps; } }

        /// <summary>
        /// Aktualizace FPS.
        /// </summary>
        /// <param name="gameTime">Herní čas.</param>
        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            fpsCounter++;

            if (elapsedTime > 1000)
            {
                elapsedTime -= 1000;
                currentFps = fpsCounter;
                fpsCounter = 0;
            }
        }
    }
}
