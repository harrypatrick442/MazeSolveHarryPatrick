using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeSolveHarryPatrick
{
    interface IPosition
    {
        int X { get; }
        int Y { get; }
        bool Equals(IPosition position);
    }
}
