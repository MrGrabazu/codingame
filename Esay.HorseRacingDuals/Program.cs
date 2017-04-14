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
        int N = int.Parse(Console.ReadLine());
        List<int> strenghts = new List<int>();
        for (int i = 0; i < N; i++)
            strenghts.Add(int.Parse(Console.ReadLine()));

        strenghts = strenghts.OrderBy(x => x).ToList();
        int result = int.MaxValue;
        for (int i = 0, j = 1, l = strenghts.Count; j < l; i++, j++)
        {
            int a = strenghts[i];
            int b = strenghts[j];
            int d = a - b;
            if (d < 0) d = d * (-1);
            if (d < result) result = d;
        }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(result);
    }
}