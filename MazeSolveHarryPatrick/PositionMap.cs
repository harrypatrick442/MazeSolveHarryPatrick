using System.Collections.Generic;
namespace MazeSolveHarryPatrick
{
    class PositionMap<T>
    {
        private Dictionary<int, Dictionary<int, T>> dict = new Dictionary<int, Dictionary<int, T>>();
        public void Add(Position position, T score)
        {
            if (!dict.ContainsKey(position.X))
                dict.Add(position.X, new Dictionary<int, T> { { position.Y, score } });
            else
            {
                var inner = dict[position.X];
                if (inner.ContainsKey(position.Y))
                    inner[position.Y] = score;
                else
                    inner.Add(position.Y, score);
            }
        }
        public T this[Position position]
        {
            get
            {
                return dict[position.X][position.Y];
            }
            set
            {
                if (!dict.ContainsKey(position.X))
                    dict.Add(position.X, new Dictionary<int, T> { { position.Y, value } });
                else
                {
                    var inner = dict[position.X];
                    if (!inner.ContainsKey(position.Y))
                        inner.Add(position.Y, value);
                    else
                        inner[position.Y] = value;
                }
            }
        }
        public bool ContainsKey(Position position) {
            if (dict.ContainsKey(position.X))
                return dict[position.X].ContainsKey(position.Y);
            return false;
        }
    }
}
