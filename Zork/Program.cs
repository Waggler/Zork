using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Zork
{
    class Program
    {
        private static Room CurrentRoom => Rooms[Location.Row, Location.Column];

        private static void Main(string[] args)
        {
            InitializeRoomDescriptions();
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;

            while (command != Commands.QUIT)
            {
                Console.Write($"{CurrentRoom}\n");
                Console.Write("> ");

                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
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

        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH when Location.Row > 0:
                    Location.Row -= 1;
                    didMove = true;
                    break;
                case Commands.SOUTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row += 1;
                    didMove = true;
                    break;

                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column += 1;
                    didMove = true;
                    break;

                case Commands.WEST when Location.Column > 0:
                    Location.Column -= 1;
                    didMove = true;
                    break;
            }

            return didMove;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, ignoreCase: true, out Commands command) ? command : Commands.UNKNOWN;

        private static bool IsDirection(Commands command) => Directions.Contains(command);

        private static readonly Room[,] Rooms =
        {
            { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") },
            { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") }
        };

        private static void InitializeRoomDescriptions()
        {
            Rooms[0, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight."; // Dense Woods
            Rooms[0, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred."; // North of House
            Rooms[0, 2].Description = "You are in a clearing, with a forest surrounding you on the west and south."; // Clearing

            Rooms[1, 0].Description = "This is a forest, with trees in all directions around you."; // Forest
            Rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door."; // West of House
            Rooms[1, 2].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar."; // Behind House

            Rooms[2, 0].Description = "You are on a rock-strewn trail."; // Rocky Trail
            Rooms[2, 1].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred."; // South of House
            Rooms[2, 2].Description = "You are at the top of the Great Canyon on its south wall."; // Canyon View
        }

        private static readonly List<Commands> Directions = new List<Commands>
        {
        Commands.NORTH,
        Commands.SOUTH,
        Commands.EAST,
        Commands.WEST
        };

        private static (int Row, int Column) Location = (1, 1);
    }

}