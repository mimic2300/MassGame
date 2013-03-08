namespace IsometricGame
{
#if WINDOWS || XBOX
    static class App
    {
        static void Main(string[] args)
        {
            using (IsometricGame game = new IsometricGame())
            {
                game.Run();
            }
        }
    }
#endif
}

