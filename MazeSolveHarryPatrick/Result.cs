using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolveHarryPatrick
{
    class Result//makes passing it about more efficient than with struct.
    {
        private const char WALL_MARKER = '#';
        private const char PATH_MARKER = 'X';
        private const char START_MARKER = 'S';
        private const char END_MARKER = 'E';
        private const char PASSAGE_MARKER = ' ';
        private bool _Solved = false;
        public bool Solved { get { return _Solved; } }
        private String _String;
        public Result(Maze maze)
        {
            _Maze = maze;

        }
        private List<IPosition> _Path;
        private Maze _Maze;
        public Result(List<IPosition> path, Maze maze)
        {
            _Path = path;
            _Maze = maze;
            _Solved = true;
        }
        public override string ToString()
        {
            if (!_Solved)
                return "Failed to solve maze";
            if (_String != null)
                return _String;
            char[][] str = new char[_Maze.Width][];
            for (int i = 0; i < _Maze.Width; i++)
            {
                char[] row = new char[_Maze.Height];
                for (int j = 0; j < _Maze.Height; j++)
                {
                    row[j] = _Maze.Grid[i, j] ? PASSAGE_MARKER : WALL_MARKER;
                }
                str[i] = row;
            }
            foreach (IPosition position in _Path)
            {
                str[position.X][position.Y] = PATH_MARKER;
            }
            str[_Maze.Start.X][_Maze.Start.Y] = START_MARKER;
            str[_Maze.End.X][_Maze.End.Y] = END_MARKER;
            _String = string.Join("\n", (from char[] row in str select new string(row)));
            return _String;
        }
    }
}
