using Newtonsoft.Json;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace Zork
{
    public class Game
    {
        public World World { get; set; }

        public string StartingLocation { get; set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        public string WelcomeMessage { get; set; }

        public string ExitMessage { get; set; }


        private void OnDeserialized(StreamingContext context)
        {
            Player = new Player(World, StartingLocation);
        }

        public void Run()
        {
            Console.WriteLine(WelcomeMessage);

            Commands command = Commands.UNKNOWN;

            while (command != Commands.QUIT)
            {
                Console.Write($"{Player.CurrentRoom}\n");
                if (Player.PreviousRoom != Player.CurrentRoom)
                {
                    Console.Write($"{Player.CurrentRoom.Description}\n");
                    Player.PreviousRoom = Player.CurrentRoom;
                }
                Console.Write("> ");

                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine(ExitMessage);
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(Player.CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Player.Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, ignoreCase: true, out Commands command) ? command : Commands.UNKNOWN;

        private enum Fields
        {
            Name = 0,
            Description = 1
        }
    }
}
