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
          //  Console.Out.NewLine = "\r\n\r\n";
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
                for (int j = 0; j < width ; j++)
                {
                    Console.Write(mazeArr[i, j]);
                }

                Console.WriteLine();
            }

            


        }
    }

}