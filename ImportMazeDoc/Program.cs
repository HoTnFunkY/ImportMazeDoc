using System;
using System.Collections.Generic;
using System.IO;

namespace ImportMazeDoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Output of new 2D array Maze!");
            Console.WriteLine();

            //Read text-file
            string[] Content = File.ReadAllLines("maze.txt");
            //get first line of text-file
            string sizeString = Content[0];
            //Find Size of the maze
            string[] myMaze = sizeString.Split('x');

            //extract dimentions of the maze
            int width = int.Parse(myMaze[0]);
            int height = int.Parse(myMaze[1]);
            //instantiation of 2D array
            string[,] mazeArr = new string[height, width];

            //Print out array to the Console.
            for (int i = 1; i < Content.Length; i++)
            {
                for (int j = 0; j < Content[i].Length; j++)
                {
                    mazeArr[i - 1, j] = Content[i][j].ToString(); // Uses Indexer get() method.
                }
            }
            //*******************************************************************************************
            Coordinate startCoordinate = null;
            Coordinate endCoordinate = null;
            //search for entry of maze
            for (int i = 0; i < height; i++)
            {
                if (mazeArr[i, 0].ToString().Equals("B"))
                {
                    startCoordinate = new Coordinate(0 , i);                   
                }
            }

            Console.WriteLine($"Start Coordinates: x:{startCoordinate.x} y:{startCoordinate.y}");

            //search for exit of maze
            for (int i = 0; i < width; i++)
            {
                if (mazeArr[i, width - 1].ToString().Equals("E"))
                {
                    endCoordinate = new Coordinate(width - 1, i);                   
                }
            }

            Console.WriteLine($"Exit Coordinates: x:{endCoordinate.x} y:{endCoordinate.y}");

            //********************************************************************************************

            // instatiation of stack to support our DFT
            Stack<IMazeNode> nodes = new Stack<IMazeNode>();
            //Add Root Node
            nodes.Push(new MazeNode(startCoordinate.x, startCoordinate.y));

            while (nodes.Count > 0)
            {
                IMazeNode next = nodes.Peek();
                IMazeNode neighbour = UnvisitedNeigbours(next, mazeArr, width, height, nodes.Count);

                if (neighbour != null)
                {
                    if (IsExit(neighbour, width, height, nodes.Count))
                    {
                        Console.WriteLine("Hurra!");
                        break;
                    }

                    mazeArr[neighbour.Coordinate.y, neighbour.Coordinate.x] = "*";
                    neighbour.Visited = true;
                    nodes.Push(neighbour);
                }
                else
                {
                    Coordinate coord = nodes.Peek().Coordinate;
                    mazeArr[coord.y, coord.x] = ".";
                    nodes.Pop();
                }

            }

            PrintSolution(width, height, mazeArr);

        }

        private static void PrintSolution(int width, int height, string[,] mazeArr)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (mazeArr[i, j] == '*'.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(mazeArr[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(mazeArr[i, j]);
                    }

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
                if (above == -1)    //if at a boundry
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
            Coordinate above = new Coordinate(node.Coordinate.x, node.Coordinate.y - 1);
            Coordinate below = new Coordinate(node.Coordinate.x, node.Coordinate.y + 1);
            Coordinate left = new Coordinate(node.Coordinate.x - 1, node.Coordinate.y);
            Coordinate right = new Coordinate(node.Coordinate.x + 1, node.Coordinate.y);

            Coordinate[] neighbours = { above, below, left, right };

            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i].x != -1 && neighbours[i].y != -1 &&
                    neighbours[i].x != width && neighbours[i].y != height)
                {
                    if (mazeArr[neighbours[i].y, neighbours[i].x].Equals(" ") ||
                               mazeArr[neighbours[i].y, neighbours[i].x].Equals("E"))
                    {
                        return new MazeNode(neighbours[i].x, neighbours[i].y);
                    } 
                }
            }          

            return null;
        }
    }

}