using System;

namespace Zork
{
    class Program
    {
        private static string Location => Rooms[LocationColumn];

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;

            while (command != Commands.QUIT)
            {
                Console.Write($"{Location}\n> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;

                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                        outputString = "The way is shut!";
                        break;

                    case Commands.EAST:
                        outputString = Move(command) ? $"You moved {command}." : "The way is shut!";
                        break;

                    case Commands.WEST:
                        outputString = Move(command) ? $"You moved {command}." : "The way is shut!";
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH:
                case Commands.SOUTH:
                    break;

                case Commands.EAST when LocationColumn < Rooms.Length - 1:
                    LocationColumn += 1;
                    didMove = true;
                    break;

                case Commands.WEST when LocationColumn > 0:
                    LocationColumn -= 1;
                    didMove = true;
                    break;
            }

            return didMove;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, ignoreCase: true, out Commands command) ? command : Commands.UNKNOWN;

        private static string[] Rooms = { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" };
        private static int LocationColumn = 1;
    }
}
