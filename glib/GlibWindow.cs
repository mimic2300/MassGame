using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using System.Windows.Forms;

using Color = Microsoft.Xna.Framework.Color;

namespace glib
{
    /// <summary>
    /// Hern� okno.
    /// </summary>
    public class GlibWindow : Game
    {
        #region Ud�losti

        /// <summary>
        /// Handler pro zachycen� vstupu z kl�vesnice.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="keyboard">Kl�vesnice.</param>
        public delegate void KeyboardInputHandler(GlibWindow window, KeyboardState keyboard);

        /// <summary>
        /// Ud�lost pro zachycen� vstupu z kl�vesnice.
        /// </summary>
        public static event KeyboardInputHandler On_Keyboard;

        /// <summary>
        /// Zavol� ud�lost pro zachycen� vstupu z kl�vesnice.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="keyboard">Kl�vesnice.</param>
        protected void Do_Keyboard(GlibWindow window, KeyboardState keyboard)
        {
            if (On_Keyboard != null)
                On_Keyboard(window, keyboard);
        }

        /// <summary>
        /// Handler pro zachycen� vstupu my�i.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="mouse">My�.</param>
        public delegate void MouseInputHandler(GlibWindow window, MouseState mouse);

        /// <summary>
        /// Ud�lost pro zachycen� vstupu my�i.
        /// </summary>
        public static event MouseInputHandler On_Mouse;

        /// <summary>
        /// Zavol� ud�lost pro zachycen� vstupu my�i.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="mouse">My�.</param>
        protected void Do_Mouse(GlibWindow window, MouseState mouse)
        {
            if (On_Mouse != null)
                On_Mouse(window, mouse);
        }

        /// <summary>
        /// Handler pro initializaci hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        public delegate void InitializeHandler(GlibWindow window);

        /// <summary>
        /// Ud�lost pro initializaci hry.
        /// </summary>
        public static event InitializeHandler On_Initialize;

        /// <summary>
        /// Zavol� ud�lost pro initializaci hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        protected void Do_Initialize(GlibWindow window)
        {
            if (On_Initialize != null)
                On_Initialize(window);
        }

        /// <summary>
        /// Handler pro na�ten� obsahu hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="graphics"></param>
        public delegate void LoadContentHandler(GlibWindow window, GraphicsDevice graphics);

        /// <summary>
        /// Ud�lost pro na�ten� obsahu hry.
        /// </summary>
        public static event LoadContentHandler On_LoadContent;

        /// <summary>
        /// Zavol� ud�lost pro na�ten� obsahu hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="graphics">Grafick� za��zen�.</param>
        protected void Do_LoadContent(GlibWindow window, GraphicsDevice graphics)
        {
            if (On_LoadContent != null)
                On_LoadContent(window, graphics);
        }

        /// <summary>
        /// Handler pro uvoln�n� obsahu hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        public delegate void UnloadContentHandler(GlibWindow window);

        /// <summary>
        /// Ud�lost pro uvoln�n� obsahu hry.
        /// </summary>
        public static event UnloadContentHandler On_UnloadContent;

        /// <summary>
        /// Zavol� ud�lost pro uvoln�n� obsahu hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        protected void Do_UnloadContent(GlibWindow window)
        {
            if (On_UnloadContent != null)
                On_UnloadContent(window);
        }

        /// <summary>
        /// Handler pro aktualizaci logiky hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="gameTime"></param>
        public delegate void UpdateHandler(GlibWindow window, GameTime gameTime);

        /// <summary>
        /// Ud�lost pro aktualizaci logiky hry.
        /// </summary>
        public static event UpdateHandler On_Update;

        /// <summary>
        /// Zavol� ud�lost pro aktualizaci logiky hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="gameTime">Hern� �as.</param>
        protected void Do_Update(GlibWindow window, GameTime gameTime)
        {
            if (On_Update != null)
                On_Update(window, gameTime);
        }

        /// <summary>
        /// Handler pro vykreslen� obsahu hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="sprite">Kresl�c� sprite.</param>
        /// <param name="gameTime">Hern� �as.</param>
        public delegate void DrawHandler(GlibWindow window, SpriteBatch sprite, GameTime gameTime);

        /// <summary>
        /// Ud�lost pro vykreslen� obsahu hry.
        /// </summary>
        public static event DrawHandler On_Draw;

        /// <summary>
        /// Zavol� ud�lost pro vykreslen� obsahu hry.
        /// </summary>
        /// <param name="window">Hern� okno.</param>
        /// <param name="sprite">Kresl�c� sprite.</param>
        /// <param name="gameTime">Hern� �as.</param>
        protected void Do_Draw(GlibWindow window, SpriteBatch sprite, GameTime gameTime)
        {
            if (On_Draw != null)
                On_Draw(window, sprite, gameTime);
        }

        #endregion Ud�losti

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private FpsCounter fpsCounter;

        private ResolutionType resolution;
        private bool windowFrame = true;

        private KeyboardState keyboard;
        private MouseState mouse;

        #region Konstruktory

        /// <summary>
        /// Hlavn� konstruktor.
        /// </summary>
        public GlibWindow()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            fpsCounter = new FpsCounter();

            IsMouseVisible = true;
            IsFixedTimeStep = false;
            IsVSync = false;
            IsAntialiasing = false;
            Resolution = ResolutionType.R_720x480;
        }

        #endregion Konstruktory

        #region Vlastnosti

        /// <summary>
        /// Z�sk� SpriteBatch.
        /// </summary>
        protected SpriteBatch Sprite
        {
            get { return spriteBatch; }
        }

        /// <summary>
        /// Z�sk� grafick� mana�er.
        /// </summary>
        protected GraphicsDeviceManager GDM
        {
            get { return graphics; }
        }

        /// <summary>
        /// Z�sk� FPS.
        /// </summary>
        public int FPS
        {
            get { return fpsCounter.FPS; }
        }

        /// <summary>
        /// Z�sk� FPS v p�vodn�m tvaru.
        /// </summary>
        public float FPSReal
        {
            get { return fpsCounter.FPSReal; }
        }

        /// <summary>
        /// ���ka okna.
        /// </summary>
        public int Width
        {
            get { return Window.ClientBounds.Width; }
        }

        /// <summary>
        /// V��ka okna.
        /// </summary>
        public int Height
        {
            get { return Window.ClientBounds.Height; }
        }

        /// <summary>
        /// ���ka plochy.
        /// </summary>
        public int DesktopWidth
        {
            get { return GraphicsDevice.Adapter.CurrentDisplayMode.Width; }
        }

        /// <summary>
        /// V��ka plochy.
        /// </summary>
        public int DesktopHeight
        {
            get { return GraphicsDevice.Adapter.CurrentDisplayMode.Height; }
        }

        /// <summary>
        /// Z�sk� kl�vesnici.
        /// </summary>
        public KeyboardState KeyboardState
        {
            get { return keyboard; }
        }

        /// <summary>
        /// Z�sk� my�.
        /// </summary>
        public MouseState MouseState
        {
            get { return mouse; }
        }

        /// <summary>
        /// Z�sk� nebo nastav� rozli�en� hern�ho okna.
        /// </summary>
        public ResolutionType Resolution
        {
            get { return resolution; }
            set
            {
                if (resolution != value)
                {
                    Size size = Size.Empty;
                    resolution = value;

                    switch (value)
                    {
                        case ResolutionType.AsDesktop:
                            size = new Size(DesktopWidth, DesktopHeight);
                            break;

                        case ResolutionType.R_320x200:
                            size = new Size(320, 200);
                            break;

                        case ResolutionType.R_320x240:
                            size = new Size(320, 240);
                            break;

                        case ResolutionType.R_640x480:
                            size = new Size(640, 480);
                            break;

                        case ResolutionType.R_720x480:
                            size = new Size(720, 480);
                            break;

                        case ResolutionType.R_800x600:
                            size = new Size(800, 600);
                            break;

                        case ResolutionType.R_1024x768:
                            size = new Size(1024, 768);
                            break;

                        case ResolutionType.R_1280x720_HD:
                            size = new Size(1280, 720);
                            break;

                        case ResolutionType.R_1280x800:
                            size = new Size(1280, 800);
                            break;

                        case ResolutionType.R_1440x900:
                            size = new Size(1440, 900);
                            break;

                        case ResolutionType.R_1680x1050:
                            size = new Size(1680, 1050);
                            break;

                        case ResolutionType.R_1920x1080_HD:
                            size = new Size(1920, 1080);
                            break;

                        case ResolutionType.R_1920x1200:
                            size = new Size(1920, 1200);
                            break;

                        case ResolutionType.R_2048x1080:
                            size = new Size(2048, 1080);
                            break;

                        case ResolutionType.R_2560x1600:
                            size = new Size(2560, 1600);
                            break;

                        default:
                            size = new Size(720, 480);
                            break;
                    }
                    graphics.PreferredBackBufferWidth = size.Width;
                    graphics.PreferredBackBufferHeight = size.Height;
                    graphics.ApplyChanges();
                }
            }
        }

        /// <summary>
        /// Zji��uje, zda se my� nach�z� v okn� nebo mimo okno.
        /// </summary>
        public bool IsMouseInWindow
        {
            get { return (MouseState.X >= 0 && MouseState.X <= Width && MouseState.Y >= 0 && MouseState.Y <= Height); }
        }

        /// <summary>
        /// Vyhlazen� obrazu.
        /// </summary>
        public bool IsAntialiasing
        {
            get { return graphics.PreferMultiSampling; }
            set
            {
                if (graphics.PreferMultiSampling != value)
                {
                    graphics.PreferMultiSampling = value;
                    graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 16; // v�t�inou se nastav� max 8x
                    graphics.ApplyChanges();
                }
            }
        }

        /// <summary>
        /// Vertik�ln� synchronizace.
        /// </summary>
        public bool IsVSync
        {
            get { return graphics.SynchronizeWithVerticalRetrace; }
            set { graphics.SynchronizeWithVerticalRetrace = value; }
        }

        /// <summary>
        /// Zjist�, zda je okno ve fullscreenu.
        /// </summary>
        public bool IsFullscreen
        {
            get { return graphics.IsFullScreen; }
            set
            {
                if (graphics.IsFullScreen)
                {
                    if (value)
                    {
                        graphics.ApplyChanges();
                    }
                    else graphics.ToggleFullScreen();
                }
                else
                {
                    if (value)
                    {
                        graphics.ToggleFullScreen();
                    }
                    else graphics.ApplyChanges();
                }
            }
        }

        /// <summary>
        /// Zjist� nebo nastav� r�me�ek okna.
        /// </summary>
        public bool IsWindowFrame
        {
            get { return windowFrame; }
            set
            {
                if (windowFrame != value)
                {
                    Form form = (Form)Form.FromHandle(Window.Handle);
                    form.FormBorderStyle = value ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
                    windowFrame = value;
                }
            }
        }

        #endregion Vlastnosti

        #region P�epsan� funkce

        /// <summary>
        /// Initializace hern�ho okna.
        /// </summary>
        protected override void Initialize()
        {
            Do_Initialize(this);

            base.Initialize();
        }

        /// <summary>
        /// Na�ten� obsahu hry.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Uvoln�n� / odebr�n� hern�ho obsahu.
        /// </summary>
        protected override void UnloadContent()
        {
            Do_UnloadContent(this);

            base.UnloadContent();
        }

        /// <summary>
        /// Aktualizace hern� logiky.
        /// </summary>
        /// <param name="gameTime">Hern� �as.</param>
        protected override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            fpsCounter.Update(gameTime);

            Do_Keyboard(this, keyboard);
            Do_Mouse(this, mouse);
            Do_Update(this, gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Vykreslen� hern�ho obsahu.
        /// </summary>
        /// <param name="gameTime">Hern� �as.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Do_Draw(this, spriteBatch, gameTime);

            base.Draw(gameTime);
        }

        #endregion P�epsan� funkce

        /// <summary>
        /// Na�te soubor z obsahu hry podle n�zvu souboru bez p��pony.
        /// </summary>
        /// <typeparam name="T">Typ souboru (Texture2D, SpriteFont apod.).</typeparam>
        /// <param name="assetName">N�zev souboru bez p��pony.</param>
        /// <returns>Vrac� na�ten� soubor.</returns>
        protected T Load<T>(string assetName)
        {
            return Content.Load<T>(assetName);
        }
    }
}
