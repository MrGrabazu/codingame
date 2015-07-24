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
        string input = Console.ReadLine();

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(ToUnary(ToBinary(Encoding.ASCII.GetBytes(input))));
    }

    public static string ToBinary(byte[] data)
    {
        return String.Join("", data.Select(x => Convert.ToString(x, 2).PadLeft(7, '0')));
    }

    public static string ToUnary(string bits)
    {
        List<Tuple<char, string>> groups = new List<Tuple<char, string>>();
        char current = bits.FirstOrDefault();
        StringBuilder builder = new StringBuilder();
        foreach(char bit in bits)
        {
            if (bit != current && builder.Length > 0)
            {
                groups.Add(new Tuple<char, string>(current, builder.ToString()));
                current = bit;
                builder = new StringBuilder();
            }
            builder.Append(bit);
        }
        groups.Add(new Tuple<char, string>(current, builder.ToString()));

        List<string> result = new List<string>();
        foreach(Tuple<char, string> group in groups)
        {
            string s = "";
            int l = 0;
            if (group.Item1 == '0')
            {
                s = "00 ";
                l = 3;
            }
            else
            {
                s = "0 ";
                l = 2;
            }

            s = s.PadRight(group.Item2.Length + l, '0');
            result.Add(s);
        }

        return String.Join(" ", result);
    }
}