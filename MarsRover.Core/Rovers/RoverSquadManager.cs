using System;
using System.Collections.Generic;
using MarsRover.Core.Enums;
using MarsRover.Core.Mars;

namespace MarsRover.Core.Rovers
{
    public class RoverSquadManager : IRoverSquadManager
    {
        private List<Rover> _rovers;

        private Rover _activeRover;

        private readonly IPlateau _plateau;

        public RoverSquadManager(IPlateau plateau)
        {
            _rovers = new List<Rover>();

            _plateau = plateau;
        }

        public void DeployRover(int x, int y, Direction direction)
        {
            if (IsValidDeploymentLocation(x, y))
            {
                var rover = new Rover(_plateau, x, y, direction);

                _rovers.Add(rover);

                _activeRover = rover;
            }
            else
            {
                throw new ArgumentException("Rover location is out of bounds.");
            }
        }

        public Rover ActiveRover()
        {
            return _activeRover;
        }

        public int CountOfRovers()
        {
            return _rovers.Count;
        }

        public List<Rover> ListRovers()
        {
            return _rovers;
        }

        private bool IsValidDeploymentLocation(int x, int y)
        {
            return (x >= 0 && x <= _plateau.Width()) && (y >= 0 && y <= _plateau.Height());
        }
    }
}