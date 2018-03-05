using System;
namespace MazeSolveHarryPatrick
{
    class SimplePosition : IPosition//makes passing it about more efficient than with struct.
    {
        private int _X;
        public int X { get { return _X; } }
        private int _Y;
        public int Y { get { return _Y; } }
        public SimplePosition(int x, int y) { _X = x; _Y = y; }
        public virtual bool Equals(IPosition b)
        {
            return (X == b.X) && (Y == b.Y);
        }
    }
}
