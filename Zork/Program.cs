using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Zork
{
    class Program
    {
        private static Room CurrentRoom => Rooms[Location.Row, Location.Column];

        private static void Main(string[] args)
        {
            const string roomDescriptionsFilename = "Rooms.txt";
            InitializeRoomDescriptions(roomDescriptionsFilename);

            Console.WriteLine("Welcome to Zork!");

            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;

            while (command != Commands.QUIT)
            {
                Console.Write($"{CurrentRoom}\n");
                if (previousRoom != CurrentRoom)
                {
                    Console.Write($"{CurrentRoom.Description}\n");
                    previousRoom = CurrentRoom;
                }
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

        private static void InitializeRoomDescriptions(string roomDescriptionsFilename)
        {
            Dictionary<string, Room> roomMap = new Dictionary<string, Room>();

            foreach (Room room in Rooms)
            {
                roomMap.Add(room.Name, room);
            }
            const string delimiter = "##";
            const int expectedFieldCount = 2;

            string[] lines = File.ReadAllLines(roomDescriptionsFilename);
            foreach (string line in lines)
            {
                string[] fields = line.Split(delimiter);

                Assert.IsTrue(fields.Length == expectedFieldCount, "Invalid record.");

                (string name, string description) = (fields[(int)Fields.Name], fields[(int)Fields.Description]);

                roomMap[name].Description = description;
            }

        }

        private static readonly List<Commands> Directions = new List<Commands>
        {
        Commands.NORTH,
        Commands.SOUTH,
        Commands.EAST,
        Commands.WEST
        };

        private enum Fields
        {
            Name = 0,
            Description = 1
        }

        private static (int Row, int Column) Location = (1, 1);
    }

}