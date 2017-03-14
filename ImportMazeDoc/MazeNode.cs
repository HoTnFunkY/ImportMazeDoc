using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportMazeDoc
{
    public class MazeNode : IMazeNode
    {
        public ICoordinate Coordinate { get; private set; }

        public ICollection<IMazeEdge> Edges { get;}

        public bool Visited { get; set; }
       /// <summary>
       /// Inititalizes a node with a coordinate set
       /// </summary>
       /// <param name="x">x Coordinate</param>
       /// <param name="y">y Coordinate</param>
        public MazeNode(int x, int y)
        {
            Coordinate = new Coordinate();
            Coordinate.x = x;
            Coordinate.y = y;

            Edges = new LinkedList<IMazeEdge>();         
        }

        public void AddEdge(IMazeEdge newEdge)
        {
            Edges.Add(newEdge);
        }
    }
}
