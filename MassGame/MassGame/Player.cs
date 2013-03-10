using glib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MassGame
{
    /// <summary>
    /// Objekt hráče.
    /// </summary>
    class Player : Sprite, IUpdate
    {
        private float speed = 0f;
        private float dt = 0f;

        private Block currBlock;
        private Block prevBlock;

        #region Konstruktory
        int ss = 0;
        /// <summary>
        /// Hlavní konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        /// <param name="texture">Textůra hráče.</param>
        public Player(GlibWindow window, Texture2D texture)
            : base(window, texture)
        {
            GlibWindow.On_Keyboard += KeyboardInput;
            Acceleration = 0.0018f;
            MaxSpeed = 0.6f;
            Delta = 0.0050f;
            CurrentBlock = null;
        }

        #endregion Konstruktory

        #region Vlastnosti

        /// <summary>
        /// Rychlost hráče.
        /// </summary>
        public float Speed
        {
            get { return speed; }
        }

        /// <summary>
        /// Maximální rychlost.
        /// </summary>
        public float MaxSpeed { get; set; }

        /// <summary>
        /// Získá nebo nastaví zrychlení a zpomalení hráče.
        /// </summary>
        public float Acceleration { get; set; }

        /// <summary>
        /// Velikost úhlu pro změnu směru hráče.
        /// </summary>
        public float Delta { get; set; }

        public Block CurrentBlock
        {
            get { return currBlock; }
            set
            {
                prevBlock = currBlock;
                currBlock = value;
            }
        }

        public Block PreviousBlock
        {
            get { return prevBlock; }
        }

        #endregion Vlastnosti

        /// <summary>
        /// Aktualizace hráče.
        /// </summary>
        /// <param name="gameTime">Herní čas.</param>
        public void Update(GameTime gameTime)
        {
            dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // přechod v horizontální ose
            if (X + Width < 0)
            {
                X += Window.Width;
            }
            else if (X > Window.Width)
            {
                X -= Window.Width;
            }

            // přechod ve vertikální ose
            if (Y + Height < 0)
            {
                Y += Window.Height;
            }
            else if (Y > Window.Height)
            {
                Y -= Window.Height;
            }

            speed = MathHelper.Clamp(speed, -MaxSpeed, MaxSpeed);
            Position += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * speed * dt;
        }

        /// <summary>
        /// Ovládání hráče.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        /// <param name="keyboard">Klávesnice.</param>
        private void KeyboardInput(GlibWindow window, KeyboardState keyboard)
        {
            // otáčení hráče
            if (keyboard.IsKeyDown(Keys.Left))
            {
                Rotation -= Delta * dt;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                Rotation += Delta * dt;
            }

            // směr pohybu hráče
            if (keyboard.IsKeyDown(Keys.Up))
            {
                speed += Acceleration * dt;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                speed -= Acceleration * dt;
            }
            else if (Speed != 0)
            {
                speed -= Math.Sign(Speed) * Acceleration * dt;

                if (speed < 0.001f && speed > -0.001f)
                    speed = 0f;
            }

            // zastavení hráče
            if (keyboard.IsKeyDown(Keys.Space))
                speed = 0f;
        }
    }
}
