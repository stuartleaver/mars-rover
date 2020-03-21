using System.Collections.Generic;
using MarsRover.Core.Enums;

namespace MarsRover.Core.Rovers
{
    public interface IRoverSquadManager
    {
        void DeployRover(int x, int y, Direction direction);

        Rover ActiveRover();

        int CountOfRovers();

        List<Rover> ListRovers();
    }
}