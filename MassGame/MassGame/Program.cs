using System;

namespace MassGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MassGame game = new MassGame())
            {
                game.Run();
            }
        }
    }
#endif
}

