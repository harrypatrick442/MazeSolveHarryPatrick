
namespace MazeSolveHarryPatrick
{
    class Position : SimplePosition, IGetSurroundingPositions<Position>
    {
        public Position(int x, int y) : base(x, y) { }
        public virtual Position ToLeft { get { return new Position(X - 1, Y); } }
        public virtual Position ToRight { get { return new Position(X + 1, Y); } }
        public virtual Position Above { get { return new Position(X, Y + 1); } }
        public virtual Position Bellow { get { return new Position(X, Y - 1); } }
    }
}
