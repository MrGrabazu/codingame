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
        int n = int.Parse(Console.ReadLine());
        string[] inputs = Console.ReadLine().Split(' ');
        List<int> values = new List<int>();
        for (int i = 0; i < n; i++)
            values.Add(int.Parse(inputs[i]));

        int global = 0;
        int stream = 0;
        int higherValue = values[0];
        int lastValue = values[0];
        int count = values.Count;
        for(int i = 1; i < count; i++)
        {
            int value = values[i];
            if (value > higherValue)
            {
                higherValue = value;
                stream = 0;
            }
            else
            {
                stream += value - lastValue;
                global = global > stream ? stream : global;
            }
            lastValue = value;
        }                

        Console.WriteLine(global);
    }
}