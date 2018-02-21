using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeSolveHarryPatrick
{
    class BreadthFirstPosition:SimplePosition, IGetSurroundingPositions<BreadthFirstPosition>//could extend Position but would add complexity to 
    {
        private BreadthFirstPosition _Parent;

        public BreadthFirstPosition ToLeft { get { return new BreadthFirstPosition(this, X - 1, Y); } }

        public BreadthFirstPosition ToRight { get { return new BreadthFirstPosition(this, X + 1, Y); } }

        public BreadthFirstPosition Above { get { return new BreadthFirstPosition(this, X, Y+1); } }

        public BreadthFirstPosition Bellow { get { return new BreadthFirstPosition(this, X, Y - 1); } }

        public BreadthFirstPosition(BreadthFirstPosition parent, int x, int y):base(x, y)
        {
            _Parent = parent;
        }
        public BreadthFirstPosition(Position position):base(position.X, position.Y)
        {

        }
        public List<IPosition> Path
        {
            get
            {
                var list = new List<IPosition>();
                var parent = _Parent;
                while (parent != null)
                {
                    list.Add(parent);
                    parent = parent._Parent;
                }
                return list;
            }
        }
    }
}
