using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using System.Windows.Forms;

using Color = Microsoft.Xna.Framework.Color;

namespace glib
{
    /// <summary>
    /// HernÌ okno.
    /// </summary>
    public class GlibWindow : Game
    {
        #region Ud·losti

        /// <summary>
        /// Handler pro zachycenÌ vstupu z kl·vesnice.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="keyboard">Kl·vesnice.</param>
        public delegate void KeyboardInputHandler(GlibWindow window, KeyboardState keyboard);

        /// <summary>
        /// Ud·lost pro zachycenÌ vstupu z kl·vesnice.
        /// </summary>
        public static event KeyboardInputHandler On_Keyboard;

        /// <summary>
        /// Zavol· ud·lost pro zachycenÌ vstupu z kl·vesnice.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="keyboard">Kl·vesnice.</param>
        protected void Do_Keyboard(GlibWindow window, KeyboardState keyboard)
        {
            if (On_Keyboard != null)
                On_Keyboard(window, keyboard);
        }

        /// <summary>
        /// Handler pro zachycenÌ vstupu myöi.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="mouse">Myö.</param>
        public delegate void MouseInputHandler(GlibWindow window, MouseState mouse);

        /// <summary>
        /// Ud·lost pro zachycenÌ vstupu myöi.
        /// </summary>
        public static event MouseInputHandler On_Mouse;

        /// <summary>
        /// Zavol· ud·lost pro zachycenÌ vstupu myöi.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="mouse">Myö.</param>
        protected void Do_Mouse(GlibWindow window, MouseState mouse)
        {
            if (On_Mouse != null)
                On_Mouse(window, mouse);
        }

        /// <summary>
        /// Handler pro initializaci hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        public delegate void InitializeHandler(GlibWindow window);

        /// <summary>
        /// Ud·lost pro initializaci hry.
        /// </summary>
        public static event InitializeHandler On_Initialize;

        /// <summary>
        /// Zavol· ud·lost pro initializaci hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        protected void Do_Initialize(GlibWindow window)
        {
            if (On_Initialize != null)
                On_Initialize(window);
        }

        /// <summary>
        /// Handler pro naËtenÌ obsahu hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="graphics"></param>
        public delegate void LoadContentHandler(GlibWindow window, GraphicsDevice graphics);

        /// <summary>
        /// Ud·lost pro naËtenÌ obsahu hry.
        /// </summary>
        public static event LoadContentHandler On_LoadContent;

        /// <summary>
        /// Zavol· ud·lost pro naËtenÌ obsahu hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="graphics">GrafickÈ za¯ÌzenÌ.</param>
        protected void Do_LoadContent(GlibWindow window, GraphicsDevice graphics)
        {
            if (On_LoadContent != null)
                On_LoadContent(window, graphics);
        }

        /// <summary>
        /// Handler pro uvolnÏnÌ obsahu hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        public delegate void UnloadContentHandler(GlibWindow window);

        /// <summary>
        /// Ud·lost pro uvolnÏnÌ obsahu hry.
        /// </summary>
        public static event UnloadContentHandler On_UnloadContent;

        /// <summary>
        /// Zavol· ud·lost pro uvolnÏnÌ obsahu hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        protected void Do_UnloadContent(GlibWindow window)
        {
            if (On_UnloadContent != null)
                On_UnloadContent(window);
        }

        /// <summary>
        /// Handler pro aktualizaci logiky hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="gameTime"></param>
        public delegate void UpdateHandler(GlibWindow window, GameTime gameTime);

        /// <summary>
        /// Ud·lost pro aktualizaci logiky hry.
        /// </summary>
        public static event UpdateHandler On_Update;

        /// <summary>
        /// Zavol· ud·lost pro aktualizaci logiky hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="gameTime">HernÌ Ëas.</param>
        protected void Do_Update(GlibWindow window, GameTime gameTime)
        {
            if (On_Update != null)
                On_Update(window, gameTime);
        }

        /// <summary>
        /// Handler pro vykreslenÌ obsahu hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="sprite">KreslÌcÌ sprite.</param>
        /// <param name="gameTime">HernÌ Ëas.</param>
        public delegate void DrawHandler(GlibWindow window, SpriteBatch sprite, GameTime gameTime);

        /// <summary>
        /// Ud·lost pro vykreslenÌ obsahu hry.
        /// </summary>
        public static event DrawHandler On_Draw;

        /// <summary>
        /// Zavol· ud·lost pro vykreslenÌ obsahu hry.
        /// </summary>
        /// <param name="window">HernÌ okno.</param>
        /// <param name="sprite">KreslÌcÌ sprite.</param>
        /// <param name="gameTime">HernÌ Ëas.</param>
        protected void Do_Draw(GlibWindow window, SpriteBatch sprite, GameTime gameTime)
        {
            if (On_Draw != null)
                On_Draw(window, sprite, gameTime);
        }

        #endregion Ud·losti

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private FpsCounter fpsCounter;

        private ResolutionType resolution;
        private bool windowFrame = true;

        private KeyboardState keyboard;
        private MouseState mouse;

        #region Konstruktory

        /// <summary>
        /// HlavnÌ konstruktor.
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
        /// ZÌsk· SpriteBatch.
        /// </summary>
        protected SpriteBatch Sprite
        {
            get { return spriteBatch; }
        }

        /// <summary>
        /// ZÌsk· grafick˝ manaûer.
        /// </summary>
        protected GraphicsDeviceManager GDM
        {
            get { return graphics; }
        }

        /// <summary>
        /// ZÌsk· FPS.
        /// </summary>
        public int FPS
        {
            get { return fpsCounter.FPS; }
        }

        /// <summary>
        /// ZÌsk· FPS v p˘vodnÌm tvaru.
        /// </summary>
        public float FPSReal
        {
            get { return fpsCounter.FPSReal; }
        }

        /// <summary>
        /// äÌ¯ka okna.
        /// </summary>
        public int Width
        {
            get { return Window.ClientBounds.Width; }
        }

        /// <summary>
        /// V˝öka okna.
        /// </summary>
        public int Height
        {
            get { return Window.ClientBounds.Height; }
        }

        /// <summary>
        /// äÌ¯ka plochy.
        /// </summary>
        public int DesktopWidth
        {
            get { return GraphicsDevice.Adapter.CurrentDisplayMode.Width; }
        }

        /// <summary>
        /// V˝öka plochy.
        /// </summary>
        public int DesktopHeight
        {
            get { return GraphicsDevice.Adapter.CurrentDisplayMode.Height; }
        }

        /// <summary>
        /// ZÌsk· kl·vesnici.
        /// </summary>
        public KeyboardState KeyboardState
        {
            get { return keyboard; }
        }

        /// <summary>
        /// ZÌsk· myö.
        /// </summary>
        public MouseState MouseState
        {
            get { return mouse; }
        }

        /// <summary>
        /// ZÌsk· nebo nastavÌ rozliöenÌ hernÌho okna.
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
        /// Zjiöùuje, zda se myö nach·zÌ v oknÏ nebo mimo okno.
        /// </summary>
        public bool IsMouseInWindow
        {
            get { return (MouseState.X >= 0 && MouseState.X <= Width && MouseState.Y >= 0 && MouseState.Y <= Height); }
        }

        /// <summary>
        /// VyhlazenÌ obrazu.
        /// </summary>
        public bool IsAntialiasing
        {
            get { return graphics.PreferMultiSampling; }
            set
            {
                if (graphics.PreferMultiSampling != value)
                {
                    graphics.PreferMultiSampling = value;
                    graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 16; // vÏtöinou se nastavÌ max 8x
                    graphics.ApplyChanges();
                }
            }
        }

        /// <summary>
        /// Vertik·lnÌ synchronizace.
        /// </summary>
        public bool IsVSync
        {
            get { return graphics.SynchronizeWithVerticalRetrace; }
            set { graphics.SynchronizeWithVerticalRetrace = value; }
        }

        /// <summary>
        /// ZjistÌ, zda je okno ve fullscreenu.
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
        /// ZjistÌ nebo nastavÌ r·meËek okna.
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

        #region P¯epsanÈ funkce

        /// <summary>
        /// Initializace hernÌho okna.
        /// </summary>
        protected override void Initialize()
        {
            Do_Initialize(this);

            base.Initialize();
        }

        /// <summary>
        /// NaËtenÌ obsahu hry.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UvolnÏnÌ / odebr·nÌ hernÌho obsahu.
        /// </summary>
        protected override void UnloadContent()
        {
            Do_UnloadContent(this);

            base.UnloadContent();
        }

        /// <summary>
        /// Aktualizace hernÌ logiky.
        /// </summary>
        /// <param name="gameTime">HernÌ Ëas.</param>
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
        /// VykreslenÌ hernÌho obsahu.
        /// </summary>
        /// <param name="gameTime">HernÌ Ëas.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Do_Draw(this, spriteBatch, gameTime);

            base.Draw(gameTime);
        }

        #endregion P¯epsanÈ funkce

        /// <summary>
        /// NaËte soubor z obsahu hry podle n·zvu souboru bez p¯Ìpony.
        /// </summary>
        /// <typeparam name="T">Typ souboru (Texture2D, SpriteFont apod.).</typeparam>
        /// <param name="assetName">N·zev souboru bez p¯Ìpony.</param>
        /// <returns>VracÌ naËten˝ soubor.</returns>
        protected T Load<T>(string assetName)
        {
            return Content.Load<T>(assetName);
        }
    }
}
