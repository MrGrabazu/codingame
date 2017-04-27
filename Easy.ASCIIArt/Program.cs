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
        int length = int.Parse(Console.ReadLine());
        int height = int.Parse(Console.ReadLine());
        string input = Console.ReadLine();
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?";
        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        for (int i = 0; i < height; i++)
        {
            string ROW = Console.ReadLine();
            foreach(char letter in alphabet)
            {
                string key = letter.ToString().ToLower();
                string line = ROW.Substring(0, length);
                ROW = ROW.Substring(length);
                if (map.ContainsKey(key) == false)
                    map.Add(key, new List<string>() { line });
                else
                    map[key].Add(line);
            }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
        for (int i = 0; i < height; i++)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char letter in input)
            {
                string key = letter.ToString().ToLower();
                if (alphabet.IndexOf(key, StringComparison.OrdinalIgnoreCase) == -1)
                    key = "?";
                builder.Append(map[key][i]);
            }
            Console.WriteLine(builder.ToString());
        }
    }
}