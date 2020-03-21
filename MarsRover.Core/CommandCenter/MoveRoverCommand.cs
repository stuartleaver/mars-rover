using System;
using MarsRover.Core.Enums;
using MarsRover.Core.Rovers;

namespace MarsRover.Core.CommandCenter
{
    public class MoveRoverCommand : ICommand
    {
        private readonly IRoverSquadManager _roverSquadManager;

        private string Command { get; }

        public MoveRoverCommand(IRoverSquadManager roverSquadManager, string command)
        {
            _roverSquadManager = roverSquadManager;

            Command = command;
        }

        public void ExecuteCommand()
        {
            var activeRover = _roverSquadManager.ActiveRover();

            MoveRover(activeRover, Command);
        }

        private void MoveRover(IRover rover, string command)
        {
            foreach (var movement in command)
            {
                if (!IsValidMovement(movement.ToString()))
                {
                    throw new ArgumentException($"Invalid Movement {movement} passed in the command");
                }

                rover.Move((Movement) Enum.Parse(typeof(Movement), movement.ToString()));
            }
        }

        private bool IsValidMovement(string movement)
        {
            return Enum.TryParse(movement, out Movement _);
        }
    }
}