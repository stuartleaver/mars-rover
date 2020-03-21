using MarsRover.Core.Enums;

namespace MarsRover.Core.Rovers
{
    public interface IRover
    {
        void Move(Movement movement);

        string Location();
    }
}