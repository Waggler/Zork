namespace Zork
{
    public class Player
    {
        public World World { get; }


        public Room CurrentRoom { get; set; }

        public Room PreviousRoom { get; set; }

        public Player(World world, string startingLocation)
        {
            World = world;

        }

        public bool Move(Directions direction)
        {

            bool isValidMove = CurrentRoom.Neighbors.TryGetValue(direction, out Room neighbor);

            if (isValidMove)
            {
                CurrentRoom = CurrentRoom.Neighbors[direction];
                return true;
            }

            return false;
        }
    }

}