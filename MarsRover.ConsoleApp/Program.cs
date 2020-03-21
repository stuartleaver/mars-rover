using System;
using MarsRover.Core.CommandCenter;
using MarsRover.Core.Mars;
using MarsRover.Core.Rovers;

namespace MarsRover.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            var plateau = new Plateau();

            var commandCenter = new CommandCenter();

            var roverSquadManager = new RoverSquadManager(plateau);

            var createPlateauCommand = new CreatePlateauCommand(plateau, "5 5");
            commandCenter.SendCommand(createPlateauCommand);

            var createRoverCommand = new CreateRoverCommand(roverSquadManager, "1 2 N");
            commandCenter.SendCommand(createRoverCommand);

            var moveRoverCommand = new MoveRoverCommand(roverSquadManager, "LMLMLMLMM");
            commandCenter.SendCommand(moveRoverCommand);

            createRoverCommand = new CreateRoverCommand(roverSquadManager, "3 3 E");
            commandCenter.SendCommand(createRoverCommand);

            moveRoverCommand = new MoveRoverCommand(roverSquadManager, "MMRMMRMRRM");
            commandCenter.SendCommand(moveRoverCommand);

            foreach (var rover in roverSquadManager.ListRovers())
            {
                Console.WriteLine(rover.Location());
            }

            Console.ReadLine();
        }
    }
}
