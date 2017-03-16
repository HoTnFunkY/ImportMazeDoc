using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportMazeDoc
{
    public interface IMazeNode
    {
        Coordinate Coordinate { get; }
        bool Visited { get; set; }
              
    }
}
