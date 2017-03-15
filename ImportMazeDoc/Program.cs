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
            Console.WriteLine("The current directory is {0}", path);// Det viser metodekaldet, det gemmes og skrives ud.

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

            Stack<IMazeNode> nodes = new Stack<IMazeNode>();
            //Add Root Node
            nodes.Push(new MazeNode(startCoordinate.x, startCoordinate.y));

            while (nodes.Count > 0)
            {
                IMazeNode next = nodes.Peek();
                IMazeNode neighbour = UnvisitedNeigbours(next, mazeArr, width, height, nodes.Count);

                if (neighbour != null && neighbour.Visited == false)
                {
                    if (IsExit(neighbour, width, height, nodes.Count))
                    {
                        Console.WriteLine("Hurra!");
                    }

                    mazeArr[neighbour.Coordinate.y, neighbour.Coordinate.x] = ".";
                    neighbour.Visited = true;
                    nodes.Push(neighbour);
                }
                else
                {
                    nodes.Pop();
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



        }

        private static bool IsExit(IMazeNode node, int width, int height, int count)
        {
            var above = node.Coordinate.y - 1;
            var below = node.Coordinate.y + 1;
            var left = node.Coordinate.x - 1;
            var right = node.Coordinate.x + 1;

            if (count > 1)
            {
                if (above == -1)    //if at End - Node
                {
                    return true;
                }
                else if (below == height)
                {
                    return true;
                }
                else if (left == -1)
                {
                    return true;
                }
                else if (right == width)
                {
                    return true;
                } 
            }

            return false;
        }

        public static IMazeNode UnvisitedNeigbours(IMazeNode node, string[,] mazeArr,
                                    int width, int height, int count)
        {

            var above = node.Coordinate.y - 1;
            var below = node.Coordinate.y + 1;
            var left = node.Coordinate.x - 1;
            var right = node.Coordinate.x + 1;

            if (above != -1 && above != height)
            {
                if (mazeArr[above, node.Coordinate.x].ToString() == " ") //||
                    //mazeArr[node.Coordinate.x, above].ToString() == "B" ||
                  //  mazeArr[above, node.Coordinate.x].ToString() == "E")
                {
                //    mazeArr[above, node.Coordinate.x] = ".";
                    return new MazeNode(node.Coordinate.x, above);
                }
            }

            if (below != -1 && below != height)
            {
                if (mazeArr[below, node.Coordinate.x].ToString() == " ") //||
                   // mazeArr[node.Coordinate.x, below].ToString() == "B" ||
                 //   mazeArr[below, node.Coordinate.x].ToString() == "E")
                {
              //      mazeArr[below, node.Coordinate.x] = ".";
                    return new MazeNode(node.Coordinate.x, below);
                }
            }

            if (left != -1 && left != width)
            {
                if (mazeArr[node.Coordinate.y, left].ToString() == " ") //||
                  //  mazeArr[left, node.Coordinate.y].ToString() == "B" ||
                   // mazeArr[node.Coordinate.y, left].ToString() == "E")
                {
               //     mazeArr[node.Coordinate.y, left] = ".";
                    return new MazeNode(left, node.Coordinate.y);
                }
            }

            if (right != -1 && right != width)
            {
                if (mazeArr[node.Coordinate.y, right].ToString() == " ") //||
                   // mazeArr[right, node.Coordinate.y].ToString() == "B" ||
                  //  mazeArr[node.Coordinate.y, right].ToString() == "E")
                {
                 //   mazeArr[node.Coordinate.y, right] = ".";
                    return new MazeNode(right, node.Coordinate.y);
                }
            }
            return node;

        }
    }

}