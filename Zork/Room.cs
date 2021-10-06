using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zork
{
    public class Room
    {
        public string Name { get; }

        public string Description { get; set; }

        public override string ToString() => Name;

        public Room(string name, string desciption = "")
        {
            Name = name;
            Description = desciption;
        }

        [JsonIgnore]
        public Dictionary<Directions, Room> Neighbors { get; set; }

        [JsonProperty(PropertyName = "Neighbors")]
        public Dictionary<Directions, string> NeighborNames { get; set; }

        public void UpdateNeighbors(World world)
        {
            Neighbors = new Dictionary<Directions, Room>();
            foreach(var (direction, name) in NeighborNames)
            {
                Neighbors.Add(direction, world.RoomsByName[name]);
            }
        }
    }
}