using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Zork
{
    public class Player
    {
        public World World { get; }


        [JsonIgnore]
        public Room CurrentRoom
        {
            get
            {
                return World.Rooms[Location.Row, Location.Column];
            }

        }

        [JsonIgnore]
        public Room PreviousRoom { get => previousRoom; set => previousRoom = value; }

        public Player(World world, string startingLocation)
        {
            World = world;

            for (int row = 0; row < World.Rooms.GetLength(0); row++)
            {
                for (int column = 0; column < World.Rooms.GetLength(1); column++)
                {
                    if (World.Rooms[row, column].Name.Equals(startingLocation, System.StringComparison.OrdinalIgnoreCase))
                    {
                        Location = (row, column);
                        return;
                    }
                }
            }


        }
        private bool IsDirection(Commands command) => Directions.Contains(command);

        public bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH when Location.Row > 0:
                    Location.Row -= 1;
                    didMove = true;
                    break;
                case Commands.SOUTH when Location.Row < World.Rooms.GetLength(0) - 1:
                    Location.Row += 1;
                    didMove = true;
                    break;

                case Commands.EAST when Location.Column < World.Rooms.GetLength(1) - 1:
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

        private static readonly List<Commands> Directions = new List<Commands>
        {
        Commands.NORTH,
        Commands.SOUTH,
        Commands.EAST,
        Commands.WEST
        };

        private static (int Row, int Column) Location = (1, 1);
        private Room previousRoom;
    }

}