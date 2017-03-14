using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Resources;

namespace ImportMazeDoc
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();// Efter at man har lavet sin .txt Skal man placere den i current working directory.
            Console.WriteLine("The current directory is {0}", path);// Det viser metodekladet, det gemmes og skrives ud.

            string Content = File.ReadAllText("maze.txt");
            //Console.Out.NewLine = "\r\n\r\n";
            Console.WriteLine("This is the text file read in and The string printed \n{0}", Content);

            Console.WriteLine();
            Console.WriteLine("Output of new 2D array Maze!");
            Console.WriteLine();


            string[] Lines = File.ReadAllLines("maze.txt");

            string sizeOfArray = Lines[0];

            string[] myMaze = sizeOfArray.Split('x');


            int width = int.Parse(myMaze[0]);
            int height = int.Parse(myMaze[1]);

            string[,] mazeArr = new string[height, width];

            for (int i = 1; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines[i].Length; j++)
                {
                    mazeArr[i - 1, j] = Lines[i][j].ToString(); // Uses Indexer get() method.
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(mazeArr[i, j]);
                }

                Console.WriteLine();
            }

            ICoordinate startCoordinate = null;
            ICoordinate endCoordinate = null;
            for (int i = 0; i < height; i++)
            {
                if (mazeArr[i, 0].ToString().Equals("B"))
                {
                    startCoordinate = new Coordinate();
                    startCoordinate.x = 0;
                    startCoordinate.y = i;
                }
            }
            Console.WriteLine($"StartCoordinates: x:{startCoordinate.x} y:{startCoordinate.y}");

            for (int i = 0; i < width; i++)
            {
                if (mazeArr[i, width - 1].ToString().Equals("E"))
                {
                    endCoordinate = new Coordinate();
                    endCoordinate.x = width - 1;
                    endCoordinate.y = i;
                }
            }
            Console.WriteLine($"EndCoordinates: x:{endCoordinate.x} y:{endCoordinate.y}");

            Stack<IMazeNode> nodes = new Stack<IMazeNode>();
            //Add Start Node
            nodes.Push(new MazeNode(startCoordinate.x, startCoordinate.y));
            //Find All Nodes
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (mazeArr[i, j].ToString().Equals(" "))
                    {
                        nodes.Push(new MazeNode(j, i));
                    }
                }
            }
            //Add End Node
            nodes.Push(new MazeNode(endCoordinate.x, endCoordinate.y));

            FindEdges(nodes, mazeArr, width, height);

            var node = nodes.FirstOrDefault(n => n.Coordinate.x == 11 && n.Coordinate.y == 11);

            for (int i = nodes.Count; i > 0; i--)
            {
                nodes.Pop();               
            }          
           
           

        }

        public static void FindEdges(IEnumerable<IMazeNode> nodes, string[,] mazeArr,
                                    int width, int height)
        {
            foreach (var node in nodes)
            {
                var above = node.Coordinate.y - 1;
                var below = node.Coordinate.y + 1;
                var left = node.Coordinate.x - 1;
                var right = node.Coordinate.x + 1;

                if (above == -1)    //if at End - Node
                {
                    node.AddEdge(new MazeEdge
                    {
                        Origin = node.Coordinate,
                        Destination = node.Coordinate
                    });
                }else if (above != -1 && above != height)
                {
                    if (mazeArr[node.Coordinate.x, above].ToString() == " " ||
                               mazeArr[node.Coordinate.x, above].ToString() == "B" ||
                               mazeArr[node.Coordinate.x, above].ToString() == "E")
                    {
                        node.AddEdge(new MazeEdge
                        {

                            Origin = node.Coordinate,
                            Destination = new Coordinate
                            { x = node.Coordinate.x, y = above }
                        });
                    } 
                }
                if (below == height)    //if at End - Node
                {
                    node.AddEdge(new MazeEdge
                    {
                        Origin = node.Coordinate,
                        Destination = node.Coordinate
                    });
                }else if (below != -1 && below != height)
                {
                    if (mazeArr[node.Coordinate.x, below].ToString() == " " ||
                                     mazeArr[node.Coordinate.x, below].ToString() == "B" ||
                                     mazeArr[node.Coordinate.x, below].ToString() == "E")
                    {
                        node.AddEdge(new MazeEdge
                        {
                            Origin = node.Coordinate,
                            Destination = new Coordinate
                            { x = node.Coordinate.x, y = below }
                        });
                    } 
                }
                if (left == -1) //if at End - Node
                {
                    node.AddEdge(new MazeEdge
                    {
                        Origin = node.Coordinate,
                        Destination = node.Coordinate
                    });
                }else if (left != -1 && left != width)
                {
                    if (mazeArr[left, node.Coordinate.y].ToString() == " " ||
                                     mazeArr[left, node.Coordinate.y].ToString() == "B" ||
                                     mazeArr[left, node.Coordinate.y].ToString() == "E")
                    {
                        node.AddEdge(new MazeEdge
                        {
                            Origin = node.Coordinate,
                            Destination = new Coordinate
                            { x = left, y = node.Coordinate.y }
                        });
                    } 
                }
               
                if (right == width)  //if at End - Node
                {
                    node.AddEdge(new MazeEdge
                    {
                        Origin = node.Coordinate,
                        Destination = node.Coordinate
                    });
                }
                else if (right != -1 && right != width)
                {
                    if (mazeArr[right, node.Coordinate.y].ToString() == " " ||
                                     mazeArr[right, node.Coordinate.y].ToString() == "B" ||
                                     mazeArr[right, node.Coordinate.y].ToString() == "E")
                    {
                        node.AddEdge(new MazeEdge
                        {
                            Origin = node.Coordinate,
                            Destination = new Coordinate
                            { x = right, y = node.Coordinate.y }
                        });
                    }
                }

            }
        }
    }

}