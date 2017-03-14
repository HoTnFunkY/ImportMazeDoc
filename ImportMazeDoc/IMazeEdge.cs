using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportMazeDoc
{
    public interface IMazeEdge
    {
        ICoordinate Origin { get; set; }
        ICoordinate Destination { get; set; }
    }
}
