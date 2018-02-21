using System;
using System.Collections.Generic;

namespace MazeSolveHarryPatrick
{
    class Solver
    {
        /// <summary>
        /// Attempts to solve the Maze using a Depth First search.
        /// </summary>
        /// <param name="maze"></param>
        /// <returns>Result object containing the result</returns>
        public static Result SolveDepthFirst(Maze maze)
        {
            var history = new PositionHistory();
            bool reachedEnd = false;
            List<IPosition> way = _SolveDepthFirst(maze, maze.Start, ref reachedEnd, history);
                return way != null ? new Result(way, maze):
                new Result(maze);
        }/// <summary>
        /// Attempts to solve the Maze using a Breadth First search
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        public static Result SolveBreadthFirst(Maze maze)
        {
            var history = new PositionHistory();
            List<IPosition> way = _SolveBreadthFirst(maze,new List<BreadthFirstPosition> { new BreadthFirstPosition(maze.Start) }, history);
            return way != null ?
                new Result(way, maze) : new Result(maze);
           
        }
        private static List<IPosition> _SolveDepthFirst(Maze maze, Position currentPosition, ref bool reachedEnd, PositionHistory positionHistory)
        {
            if (currentPosition.Equals(maze.End))
            {
                reachedEnd = true;
                return new List<IPosition> { currentPosition };
            }
            positionHistory.Add(currentPosition);
            Position[] surroundingPositions = GetSurroundingPositions(maze, currentPosition, positionHistory);
            foreach (Position position in surroundingPositions) {
                Console.WriteLine(position.X + "," + position.Y);
                List<IPosition> r = _SolveDepthFirst(maze, position, ref reachedEnd, positionHistory);
                if (reachedEnd) {
                    r.Add(currentPosition);
                    return r;
                }
            }
            return null;
        }
        private static List<IPosition> _SolveBreadthFirst(Maze maze, List<BreadthFirstPosition> currentPositions, PositionHistory positionHistory)
        {
            var nextPositions = new List<BreadthFirstPosition>();
            foreach (BreadthFirstPosition currentPosition in currentPositions)
            {
                if (currentPosition.Equals(maze.End))
                    return currentPosition.Path;
            }
            foreach(BreadthFirstPosition currentPosition in currentPositions)
            {
                BreadthFirstPosition[] surroundingPositions = GetSurroundingPositions(maze, currentPosition, positionHistory);
                positionHistory.AddRange(surroundingPositions);
                nextPositions.AddRange(surroundingPositions);
            }
            if (nextPositions.Count < 1)
                return null;
            return _SolveBreadthFirst(maze, nextPositions,  positionHistory);
        }
        private static T[] GetSurroundingPositions<T>(Maze maze, T currentPosition, PositionHistory history) where T: IGetSurroundingPositions<T>, IPosition{
           var nextPositions = new List<T>();
            if (currentPosition.X < maze.Width - 1)
                AddIfPositionIsPathAndNotSeen<T>(nextPositions, currentPosition.ToRight, maze, history);
            if (currentPosition.X > 0)
                AddIfPositionIsPathAndNotSeen(nextPositions, currentPosition.ToLeft, maze, history);
            if (currentPosition.Y < maze.Height - 1)
                AddIfPositionIsPathAndNotSeen(nextPositions, currentPosition.Bellow, maze, history);
            if (currentPosition.Y > 0)
                AddIfPositionIsPathAndNotSeen(nextPositions, currentPosition.Above, maze, history);
            Console.WriteLine(nextPositions.Count);
            return nextPositions.ToArray();
        }
        private static void AddIfPositionIsPathAndNotSeen<T>(List<T> positions, T position, Maze maze, PositionHistory history) where T:IPosition{
            if (maze.Grid[position.X, position.Y] && !history.HasSeen(position))
            {
                Console.WriteLine(position.X + "," + position.Y);
                positions.Add(position);
            }
        }
    }
}
