using System;
using MarsRover.Core.Enums;
using MarsRover.Core.Mars;

namespace MarsRover.Core.Rovers
{
    public class Rover : IRover
    {
        private readonly IPlateau _plateau;

        private int X { get; set; }

        private int Y { get; set; }

        private Direction Direction { get; set; }

        public Rover(IPlateau plateau, int x, int y, Direction direction)
        {
            _plateau = plateau;

            X = x;

            Y = y;

            Direction = direction;
        }

        public void Move(Movement movement)
        {
            switch (movement)
            {
                case Movement.L:
                    TurnLeft();

                    break;
                case Movement.R:
                    TurnRight();

                    break;
                case Movement.M:
                    MoveForward();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
            }
        }

        public string Location()
        {
            return $"{X} {Y} {Direction.ToString()}";
        }

        private void TurnLeft()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.W;

                    break;
                case Direction.S:
                    Direction = Direction.E;

                    break;
                case Direction.E:
                    Direction = Direction.N;

                    break;
                case Direction.W:
                    Direction = Direction.S;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TurnRight()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.E;

                    break;
                case Direction.S:
                    Direction = Direction.W;

                    break;
                case Direction.E:
                    Direction = Direction.S;

                    break;
                case Direction.W:
                    Direction = Direction.N;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveForward()
        {
            switch (Direction)
            {
                case Direction.N:
                    if (Y + 1 <= _plateau.Height())
                    {
                        Y += 1;
                    }

                    break;
                case Direction.S:
                    if (Y - 1 >= 0)
                    {
                        Y -= 1;
                    }

                    break;
                case Direction.E:
                    if (X + 1 <= _plateau.Width())
                    {
                        X += 1;
                    }

                    break;
                case Direction.W:
                    if (X - 1 >= 0)
                    {
                        X -= 1;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected bool Equals(Rover other)
        {
            return X == other.X && Y == other.Y && Direction == other.Direction;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rover) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ (int) Direction;
                return hashCode;
            }
        }
    }
}