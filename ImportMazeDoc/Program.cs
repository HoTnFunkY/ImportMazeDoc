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

            string Content = File.ReadAllText("MazeTest1.txt");
            string resourcetext1 = Resource1.MazeTest1;
            Console.Out.NewLine = "\r\n\r\n";
            Console.WriteLine("This is the text file read in and The string printed \n{0}", Content);
            Console.WriteLine("same file loaded using resources \n\r{0}", resourcetext1);*/

            string[] Lines = File.ReadAllLines("MazeTest1.txt");
            char[] Lines2 = 
            Console.Out.NewLine = "\r\n\r\n";

            foreach (string line in Lines)
            {
                Console.WriteLine(line); 
            }
            
            Console.WriteLine("This is the resource char array");
            foreach (char line in Lines2)
            {
                Console.WriteLine(line);
            }

            int arraySize = Lines.Length;
            string array2Size = arraySize.ToString();

            Console.WriteLine("Array Size: {0}", array2Size);
            Console.WriteLine(Lines[0]);

            Console.Out.NewLine = "\r\n\r\n";


            Console.WriteLine("This is the text file read in and The string printed \n{0}", Content);


            string[,] lines2D = new string[21,21];


            for (int i = 0; i < lines2D.Length; i++)
            {



                for (int j = 0; j < length; j++)
                {

                }

            }

            
        }
    }
}
