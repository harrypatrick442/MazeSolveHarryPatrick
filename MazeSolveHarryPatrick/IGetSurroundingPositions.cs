using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeSolveHarryPatrick
{
    interface IGetSurroundingPositions<T>
    {
        T ToLeft { get ;  }
        T ToRight { get; }
        T Above { get; }
        T Bellow { get; }
    }
}
