using System;
using System.IO;

namespace File_Duplicator
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceCSV = args[0];

            string[,] values = LoadCSV(sourceCSV);
            int num_rows = values.GetUpperBound(0) + 1;
 
            //Read the data.
            for (int r = 1; r < num_rows; r++)
             {
                FileInfo sourceFile = new FileInfo(values[r, 0]);
                if (!sourceFile.Exists)
                {
                    Console.WriteLine(sourceFile.Name);
                }
                FileInfo destinationFile = new FileInfo(values[r, 1]);
                if (!destinationFile.Exists)
                {
                    destinationFile.Directory.Create();
                }
                sourceFile.CopyTo(values[r, 1], true);
             }
        }

        private static string[,] LoadCSV(string filename)
        {
            // Get the file's text.
            string whole_file = System.IO.File.ReadAllText(filename);
 
            // Split into lines.
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);
 
            // See how many rows and columns there are.
            int num_rows = lines.Length;
            int num_cols = lines[0].Split(',').Length;
 
            // Allocate the data array.
            string[,] values = new string[num_rows, num_cols];
 
            // Load the array.
            for (int r = 0; r < num_rows; r++)
            {
                string[] line_r = lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[r, c] = line_r[c];
                }
            }
 
            // Return the values.
            return values;
        }
    }
}
