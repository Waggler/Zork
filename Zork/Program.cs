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
        private static readonly Dictionary<string, Room> roomMap;
        static Program()
        {
            roomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                roomMap[room.Name] = room;
            }

        }
        private static Room CurrentRoom => Rooms[Location.Row, Location.Column];

        private static void Main(string[] args)
        {
            const string roomDescriptionsFilename = "Rooms.json";
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
            var rooms = JsonConvert.DeserializeObject<Room[]>(File.ReadAllText(roomDescriptionsFilename));

            foreach (Room room in rooms)
            {
                roomMap[room.Name].Description = room.Description;
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