using System;
namespace MazeSolveHarryPatrick
{
    class Position : SimplePosition, IGetSurroundingPositions<Position>
    {
        public Position(int x, int y) : base(x, y) { }
        public virtual Position ToLeft { get { return new Position(X - 1, Y); } }
        public virtual Position ToRight { get { return new Position(X + 1, Y); } }
        public virtual Position Above { get { return new Position(X, Y + 1); } }
        public virtual Position Bellow { get { return new Position(X, Y - 1); } }
        public Position NextInDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return Bellow;
                case Direction.Left:
                    return ToLeft;
                case Direction.Right:
                    return ToRight;
                default:
                    return Above;
            }
        }
        public Position[] ToSides(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                    return new Position[2] { ToLeft, ToRight };
                default:
                    return new Position[2] { Above, Bellow };
            }
        }
    }
}
