using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zork
{
    public class Player
    {
        public World World { get; }

        [JsonIgnore]
        public Room Location { get; private set; }

        public int totalScore = 0;

        public int totalMoves = 0;

        [JsonIgnore]
        public string LocationName
        {
            get
            {
                return Location?.Name;
            }
            set
            {
                Location = World?.RoomsByName.GetValueOrDefault(value);
            }
        }

        public Player(World world, string startingLocation)
        {
            World = world;
            LocationName = startingLocation;
        }

        public bool Move(Directions direction)
        {
            bool isValidMove = Location.Neighbors.TryGetValue(direction, out Room destination);
            if (isValidMove)
            {
                Location = destination;
            }

            return isValidMove;
        }

        public void IncrementMoves()
        {
            totalMoves++;
        }

        public void IncrementScore()
        {
            totalScore++;
        }
    }

    public enum Directions
    {
        North = Commands.NORTH,
        South = Commands.SOUTH,
        East = Commands.EAST,
        West = Commands.WEST
    }
}