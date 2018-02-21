
namespace MazeSolveHarryPatrick
{
    /// <summary>
    /// Stores maze information extracted from the string passed to Maze constructor.
    /// </summary>
    class Maze
    {
        private int _Height;
        public int Height { get { return _Height; } }
        private int _Width;
        public int Width { get { return _Width; } }
        private Position _Start;
        public Position Start { get { return _Start; } }
        private Position _End;
        public Position End { get { return _End; } }
        private readonly bool[,] _Grid;
        public  bool[,] Grid { get { return _Grid; } }
        /// <summary>
        /// Represents a maze.
        /// </summary>
        /// <param name="raw">string representation of a maze</param>
        public Maze(string raw) {
            string[] lines = raw.Replace("\r", "").Split('\n');
            PrepareDimensions(lines);
            PrepareStart(lines);
            PrepareEnd(lines);
            _Grid = new bool[Width, Height];
            PrepareGrid(lines);
        }
        private void PrepareDimensions(string[] lines)
        {
            string[] split = lines[0].Split(' ');
            _Width = int.Parse(split[0]);
            _Height = int.Parse(split[1]);
        }
        private void PrepareStart(string[] lines) {
            string[] split = lines[1].Split(' ');
            _Start = new Position(int.Parse(split[0]), int.Parse(split[1]));
        }
        private void PrepareEnd(string[] lines) {
            string[] split = lines[2].Split(' ');
            _End = new Position(int.Parse(split[0]), int.Parse(split[1]));
        }
        private void PrepareGrid(string[] lines)
        {
            int j = 0;
            int to = Height + 3;
            for (int i = 3; i < to; i++, j++)
            {
                string row = lines[i];
                string[] split = row.Split(' ');
                for (int k = 0; k < Width; k++)
                {
                    _Grid[k, j] = (split[k].Equals("1") ? false : true);
                }
            }
        }












        

      




    }
}
