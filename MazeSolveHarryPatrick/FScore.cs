using System.Collections.Generic;
using System.Linq;

namespace MazeSolveHarryPatrick
{
    class FScore
    {
        private PositionMap<double> positionMap = new PositionMap<double>();
        public FScore(Maze maze, double costStartToEnd)
        {
            positionMap.Add(maze.Start,costStartToEnd);
        }
        public Position GetLowest(PositionSet openSet)
        {
            Position lowest = null;
            bool first = true;
            double lowestValue = 0;
            foreach (Position a in openSet)
            {
                if (positionMap.ContainsKey(a))
                {
                    double value = positionMap[a];
                    if (first || lowestValue < value)
                    {
                        lowest = a;
                        lowestValue = value;
                        first = false;
                    }
                }
            }
            return lowest;
        }
        public void Add(Position p, double score)
        {
            positionMap.Add(p, score);
        }
    }
}
