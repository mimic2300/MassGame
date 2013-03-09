using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace glib
{
    /// <summary>
    /// Herní sprite.
    /// </summary>
    public class Sprite : IDraw
    {
        private Rectangle rectangle;
        private Vector2 position;
        private Texture2D texture;

        #region Konstruktory

        /// <summary>
        /// Bezparametrický konstruktor.
        /// </summary>
        public Sprite()
        {
            Window = null;
            rectangle = Rectangle.Empty;
            position = Vector2.Zero;
            texture = null;
            Color = Color.White;
            IsRelative = true;
            Rotation = 0f;
            Origin = Vector2.Zero;
            Scale = new Vector2(1.0f, 1.0f);
            Effect = SpriteEffects.None;
            LayerDepth = 0f;
        }

        /// <summary>
        /// Kopírovací konstruktor.
        /// </summary>
        /// <param name="sprite">Zdrojový sprite.</param>
        public Sprite(Sprite sprite)
        {
            Window = sprite.Window;
            rectangle = sprite.rectangle;
            position = sprite.position;
            texture = sprite.texture;
            Color = sprite.Color;
            IsRelative = sprite.IsRelative;
            Rotation = sprite.Rotation;
            Origin = sprite.Origin;
            Scale = sprite.Scale;
            Effect = sprite.Effect;
            LayerDepth = sprite.LayerDepth;
        }

        /// <summary>
        /// Konstruktor s nastavením herního okna.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public Sprite(GlibWindow window)
            : this()
        {
            Window = window;
        }

        /// <summary>
        /// Hlavní konstruktor pro nastavení textůry.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        /// <param name="texture">Textůra.</param>
        public Sprite(GlibWindow window, Texture2D texture)
            : this(window)
        {
            this.texture = texture;
            rectangle = texture.Bounds;
            position = Vector2.Zero;
            Color = Color.White;
        }

        /// <summary>
        /// Konstruktor pro nastavení textůry a její pozice.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        /// <param name="texture">Textůra.</param>
        /// <param name="position">Pozice.</param>
        public Sprite(GlibWindow window, Texture2D texture, Vector2 position)
            : this(window, texture)
        {
            Position = position;
        }

        /// <summary>
        /// Konstruktor pro nastavení textůry, pozice a barvy textůry.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        /// <param name="texture">Textůra.</param>
        /// <param name="position">Pozice.</param>
        /// <param name="color">Barva.</param>
        public Sprite(GlibWindow window, Texture2D texture, Vector2 position, Color color)
            : this(window, texture, position)
        {
            Color = color;
        }

        #endregion Konstruktory

        #region Vlastnosti

        /// <summary>
        /// Získá nebo nastaví herní okno.
        /// </summary>
        public GlibWindow Window { get; set; }

        /// <summary>
        /// Získá textůru.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            protected set
            {
                texture = value;
                rectangle = new Rectangle((int)position.X, (int)position.Y, value.Width, value.Height);
            }
        }

        /// <summary>
        /// Získá nebo nastaví pozici textůry.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                rectangle.X = (int)value.X;
                rectangle.Y = (int)value.Y;
            }
        }

        /// <summary>
        /// Získá nebo nastaví velikost textůry.
        /// </summary>
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set
            {
                rectangle = value;
                position.X = value.X;
                position.Y = value.Y;
            }
        }

        /// <summary>
        /// Získá přepočítanou velikost textůry v měřítku.
        /// </summary>
        public Rectangle ScaledRectangle
        {
            get { return new Rectangle((int)X, (int)Y, (int)(Width * Scale.X), (int)(Height * Scale.Y)); }
        }

        /// <summary>
        /// Získá nebo nastaví pozici na ose X.
        /// </summary>
        public float X
        {
            get { return position.X; }
            set { Position = new Vector2(value, position.Y); }
        }

        /// <summary>
        /// Získá nebo nastaví pozici na ose Y.
        /// </summary>
        public float Y
        {
            get { return position.Y; }
            set { Position = new Vector2(position.X, value); }
        }

        /// <summary>
        /// Šířka textůry.
        /// </summary>
        public int Width
        {
            get { return rectangle.Width; }
        }

        /// <summary>
        /// Výška textůry.
        /// </summary>
        public int Height
        {
            get { return rectangle.Height; }
        }

        /// <summary>
        /// Získá nebo nastaví barvu textůry.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Získá nebo nastaví relativní textůru.
        /// Pokud bude true a textůra při najetí na jednu stranu okna se přesune na opačnou, tak
        /// tento přechod ze strany na stranu bude plynulý.
        /// </summary>
        public bool IsRelative { get; set; }

        /// <summary>
        /// Získá nebo nastaví rotaci textůry.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Bod, podle kterého se má textůra otáčet.
        /// Bod [0,0] je horní levý roh textůry.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Měřítko textůry.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// Efekt textůry spritu.
        /// </summary>
        public SpriteEffects Effect { get; set; }

        /// <summary>
        /// Vnoření vrstvy textůry.
        /// Hodnota 0 je vrchní vrstva a 1 zadní.
        /// </summary>
        public float LayerDepth { get; set; }

        /// <summary>
        /// Získá střed textůry.
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2(Width / 2.0f, Height / 2.0f); }
        }

        #endregion Vlastnosti

        /// <summary>
        /// Vykreslí textůru.
        /// </summary>
        /// <param name="sprite">Sprite pro vykreslení.</param>
        /// <param name="gameTime">Herní čas.</param>
        public virtual void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            sprite.Draw(texture, position, null, Color, Rotation, Origin, Scale, Effect, LayerDepth);

            if (IsRelative)
            {
                if (X - Origin.X < 0)
                {
                    sprite.Draw(texture, new Vector2(X + Window.Width, Y), null, Color, Rotation, Origin, Scale, Effect, LayerDepth);
                }
                else if (X - Origin.X + Width > Window.Width)
                {
                    sprite.Draw(texture, new Vector2(X - Window.Width, Y), null, Color, Rotation, Origin, Scale, Effect, LayerDepth);
                }

                if (Y - Origin.Y < 0)
                {
                    sprite.Draw(texture, new Vector2(X, Y + Window.Height), null, Color, Rotation, Origin, Scale, Effect, LayerDepth);
                }
                else if (Y - Origin.Y + Height > Window.Height)
                {
                    sprite.Draw(texture, new Vector2(X, Y - Window.Height), null, Color, Rotation, Origin, Scale, Effect, LayerDepth);
                }
            }
        }

        /// <summary>
        /// Nastaví meřítko textůry na stejnou hodnotu v ose X a Y.
        /// </summary>
        /// <param name="scale">Měřítko.</param>
        public void SetScale(float scale)
        {
            Scale = new Vector2(scale, scale);
        }

        /// <summary>
        /// Zjistí, zda se v textůre vyskytuje pozice.
        /// </summary>
        /// <param name="x">Pozice na ose X.</param>
        /// <param name="y">Pozice na ose Y.</param>
        /// <returns>Vrací true, pokud tam existuje.</returns>
        public bool Contains(int x, int y)
        {
            return ScaledRectangle.Contains(x, y);
        }

        /// <summary>
        /// Zjistí, zda se v textůre vyskytuje pozice.
        /// </summary>
        /// <param name="position">Pozice.</param>
        /// <returns>Vrací true, pokud tam existuje.</returns>
        public bool Contains(Vector2 position)
        {
            return ScaledRectangle.Contains((int)position.X, (int)position.Y);
        }

        /// <summary>
        /// Zjistí, zda se v textůre vyskytuje pozice.
        /// </summary>
        /// <param name="position">Pozice.</param>
        /// <returns>Vrací true, pokud tam existuje.</returns>
        public bool Contains(Point position)
        {
            return ScaledRectangle.Contains(position.X, position.Y);
        }

        /// <summary>
        /// Zjistí, zda se v textůre vyskytuje pozice.
        /// </summary>
        /// <param name="rectangle">Obdelník.</param>
        /// <returns>Vrací true, pokud tam existuje.</returns>
        public bool Contains(Rectangle rectangle)
        {
            return ScaledRectangle.Contains(rectangle);
        }
    }
}
