using System.Collections;
using System.Collections.Generic;
namespace MazeSolveHarryPatrick
{
    class PositionSet : IEnumerable<Position>
    {

        private Dictionary<int, HashSet<int>> dict = new Dictionary<int, HashSet<int>>();
        public void Add(Position position)
        {
            if (!dict.ContainsKey(position.X))
                dict.Add(position.X, new HashSet<int> { position.Y });
            else
            {
                dict[position.X].Add(position.Y);
            }
        }
        public void Remove(Position position)
        {
            if (dict.ContainsKey(position.X))
            {
                var inner = dict[position.X];
                if (inner.Contains(position.Y))
                {
                    inner.Remove(position.Y);
                    if (inner.Count < 1)
                        dict.Remove(position.X);
                }
            }
        }
        public int Count
        {
            get
            {
                int count = 0;
                foreach (HashSet<int> hashSet in dict.Values)
                    foreach (int i in hashSet)
                        count++;
                return count;
            }
        }
        public bool Contains(Position position)
        {
            if (dict.ContainsKey(position.X))
                return dict[position.X].Contains(position.Y);
            return false;
        }
        private IEnumerator<Position> _GetEnumerator()
        {
            foreach (int x in dict.Keys)
                foreach (int y in dict[x])
                    yield return new Position(x, y);
        }
        public IEnumerator<Position> GetEnumerator()
        {
            return _GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _GetEnumerator();
        }
    }
}
