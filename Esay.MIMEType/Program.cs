using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
        int Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.
        Dictionary<string, string> map = new Dictionary<string, string>();
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            string extension = inputs[0]; // file extension
            string mime = inputs[1]; // MIME type.

            if(String.IsNullOrEmpty(extension) == true)
                continue;

            extension = extension.ToLowerInvariant();

            if (map.ContainsKey(extension) == true)
                continue;

            map.Add(extension, mime);
        }
        for (int i = 0; i < Q; i++)
        {
            string name = Console.ReadLine(); // One file name per line.
            if (String.IsNullOrEmpty(name) == true)
            {
                Console.WriteLine("UNKNOWN");
                continue;
            }
            
            int dotIndex = name.LastIndexOf('.');
            if (dotIndex == -1)
            {
                Console.WriteLine("UNKNOWN");
                continue;
            }

            string extension = name.Substring(dotIndex + 1).ToLowerInvariant();
            Console.Error.WriteLine(extension);
            if (map.ContainsKey(extension) == false)
            {
                Console.WriteLine("UNKNOWN");
                continue;
            }

            Console.WriteLine(map[extension]);
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        //Console.WriteLine("UNKNOWN"); // For each of the Q filenames, display on a line the corresponding MIME type. If there is no corresponding type, then display UNKNOWN.
    }
}