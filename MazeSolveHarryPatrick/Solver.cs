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
            return way != null ? new Result(way, maze) :
            new Result(maze);
        }/// <summary>
         /// Attempts to solve the Maze using a Breadth First search
         /// </summary>
         /// <param name="maze"></param>
         /// <returns></returns>
        public static Result SolveBreadthFirst(Maze maze)
        {
            var history = new PositionHistory();
            List<IPosition> way = _SolveBreadthFirst(maze, new List<BreadthFirstPosition> { new BreadthFirstPosition(maze.Start) }, history);
            return way != null ?
                new Result(way, maze) : new Result(maze);

        }
        public static Result SolveAStar(Maze maze)
        {
            // The set of nodes already evaluated
            var closedSet = new PositionSet();

            // The set of currently discovered nodes that are not evaluated yet.
            // Initially, only the start node is known.
            var openSet = new PositionSet();
            openSet.Add(maze.Start);

            // For each node, which node it can most efficiently be reached from.
            // If a node can be reached from many nodes, cameFrom will eventually contain the
            // most efficient previous step.
            var cameFrom = new PositionMap<Position>();

            // For each node, the cost of getting from the start node to that node.
            var gScore = new GScore(maze, 0);
            //map with default value of Infinity

            // For each node, the total cost of getting from the start node to the goal
            // by passing by that node. That value is partly known, partly heuristic.
            var fScore = new FScore(maze, HeuristicCostEstimate(maze.Start, maze.End));
            // For the first node, that value is completely heuristic.

            while (openSet.Count > 0)
            {
                Position current = fScore.GetLowest(openSet);
                //the node in openSet having the lowest fScore[] value

                if (current.Equals(maze.End))
                {
                    return new Result(ReconstructPath(cameFrom, current), maze);
                }
                openSet.Remove(current);
                closedSet.Add(current);
                foreach (var neighbor in GetNeighbours(maze, current))
                {
                    if (closedSet.Contains(neighbor))
                    {
                        continue;       // Ignore the neighbor which is already evaluated.
                    }
                    if (!openSet.Contains(neighbor))
                    {  // Discover a new node
                        openSet.Add(neighbor);
                    }
                    // The distance from start to a neighbor
                    //the "dist_between" function may vary as per the solution requirements.
                    double tentative_gScore = gScore[current] + GetDistanceBetweenCost(current, neighbor);
                    if (tentative_gScore >= gScore[neighbor])
                    {
                        continue;		// This is not a better path.
                    }
                    // This path is the best until now. Record it!
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative_gScore;
                    fScore.Add(neighbor, gScore[neighbor] + HeuristicCostEstimate(neighbor, maze.End));
                }
            }
            return new Result(maze);
        }
        private static double GetDistanceBetweenCost(Position a, Position b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        private static List<IPosition> ReconstructPath(PositionMap<Position> cameFrom, Position current)
        {
            var totalPath = new List<IPosition> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
            }
            return totalPath;
        }
        private static double HeuristicCostEstimate(Position neighbour, Position goal)
        {
            return Math.Sqrt(Math.Pow(neighbour.X - goal.X, 2) + Math.Pow(neighbour.Y - goal.Y, 2));
        }
        private static IEnumerable<Position> GetNeighbours(Maze maze, Position current)
        {
            foreach (Direction direction in new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
                if (maze.Grid[current.X, current.Y])
                    yield return current.NextInDirection(direction);
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
            foreach (Position position in surroundingPositions)
            {
                List<IPosition> r = _SolveDepthFirst(maze, position, ref reachedEnd, positionHistory);
                if (reachedEnd)
                {
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
            foreach (BreadthFirstPosition currentPosition in currentPositions)
            {
                BreadthFirstPosition[] surroundingPositions = GetSurroundingPositions(maze, currentPosition, positionHistory);
                positionHistory.AddRange(surroundingPositions);
                nextPositions.AddRange(surroundingPositions);
            }
            if (nextPositions.Count < 1)
                return null;
            return _SolveBreadthFirst(maze, nextPositions, positionHistory);
        }
        private static T[] GetSurroundingPositions<T>(Maze maze, T currentPosition, PositionHistory history) where T : IGetSurroundingPositions<T>, IPosition
        {
            var nextPositions = new List<T>();
            if (currentPosition.X < maze.Width - 1)
                AddIfPositionIsPathAndNotSeen<T>(nextPositions, currentPosition.ToRight, maze, history);
            if (currentPosition.X > 0)
                AddIfPositionIsPathAndNotSeen(nextPositions, currentPosition.ToLeft, maze, history);
            if (currentPosition.Y < maze.Height - 1)
                AddIfPositionIsPathAndNotSeen(nextPositions, currentPosition.Bellow, maze, history);
            if (currentPosition.Y > 0)
                AddIfPositionIsPathAndNotSeen(nextPositions, currentPosition.Above, maze, history);
            return nextPositions.ToArray();
        }
        private static void AddIfPositionIsPathAndNotSeen<T>(List<T> positions, T position, Maze maze, PositionHistory history) where T : IPosition
        {
            if (maze.Grid[position.X, position.Y] && !history.HasSeen(position))
            {
                positions.Add(position);
            }
        }
    }
}
