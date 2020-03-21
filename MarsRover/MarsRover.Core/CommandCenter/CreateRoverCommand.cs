using System;
using MarsRover.Core.Enums;
using MarsRover.Core.Rovers;

namespace MarsRover.Core.CommandCenter
{
    public class CreateRoverCommand : ICommand
    {
        private IRoverSquadManager RoverSquadManager { get; set; }

        private string Command { get; set; }

        public CreateRoverCommand(IRoverSquadManager roverSquadManager, string command)
        {
            RoverSquadManager = roverSquadManager;

            Command = command;
        }

        public void ExecuteCommand()
        {
            ParseCommand(Command, out var x, out var y, out var direction);

            RoverSquadManager.DeployRover(x, y, direction);
        }

        private void ParseCommand(string command, out int x, out int y, out Direction direction)
        {
            var splitCommand = command.Split(' ');

            x = int.Parse(splitCommand[0]);

            y = int.Parse(splitCommand[1]);

            if (!Enum.TryParse(splitCommand[2], out direction))
            {
                throw new ArgumentException($"Invalid Direction {splitCommand[2]} passed in the command");
            }

        }
    }
}