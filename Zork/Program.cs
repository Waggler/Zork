using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFilename = "Zork.json";
            string gameFilename = args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename;
            Game game = Game.Load(gameFilename);
            game.Run();
        }
        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}