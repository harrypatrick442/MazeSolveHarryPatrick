using System.Collections.Generic;
using System.Linq;

namespace MazeSolveHarryPatrick
{
    class GScore
    {
        private PositionMap<double> positionMap = new PositionMap<double>();
        public GScore(Maze maze, double costStartToEnd)
        {
            positionMap.Add(maze.Start, costStartToEnd);
        }
        public void Add(Position position, double score) {
            positionMap.Add(position, score);
        }
        public double this[Position position]
        {

            // The get accessor.
            get
            {
                if(positionMap.ContainsKey(position))
                return positionMap[position];
                return double.PositiveInfinity;
            }
            set
            {
                positionMap[position] = value;
            }
        }
    }
}
