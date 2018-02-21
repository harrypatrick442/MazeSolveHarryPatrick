using System.Collections.Generic;

namespace MazeSolveHarryPatrick
{
    class PositionHistory//makes passing it about more efficient than with struct.
    {
        private Dictionary<int, HashSet<int>> _Seen = new Dictionary<int, HashSet<int>>();
        public bool HasSeen(IPosition position)
        {
            if (_Seen.ContainsKey(position.X))
                return _Seen[position.X].Contains(position.Y);
            return false;
        }
        public void Add(IPosition position) {
            if (!_Seen.ContainsKey(position.X))
                _Seen.Add(position.X, new HashSet<int> { position.Y });
            else
                _Seen[position.X].Add(position.Y);
            //do not need to check to see if maze already contains the point because add method should never be called more than once for same point
            //because of the recursive way the _Solve algorithm works.
        }
        public void AddRange(IEnumerable<IPosition> positions)
        {
            foreach (IPosition position in positions)
                Add(position);
        }
    }
}
