using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {
        private static void Main(string[] args)
        {
            const string defaultGameFilename = "Zork.json";
            string gameFilename = args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename;
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));
            game.Run();
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        };
    }

}