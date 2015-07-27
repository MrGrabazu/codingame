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
        double userLongitude = Convert.ToDouble(Console.ReadLine().Replace(',', '.'));
        double userLatitude = Convert.ToDouble(Console.ReadLine().Replace(',', '.'));

        int n = int.Parse(Console.ReadLine());
        double distance = Double.MaxValue;
        string result = "";
        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split(';');
            double longitude = Convert.ToDouble(input[4].Replace(',', '.'));
            double latitude = Convert.ToDouble(input[5].Replace(',', '.'));

            double x = (userLongitude - longitude) * Math.Cos((userLatitude + latitude) / 2);
            double y = userLatitude - latitude;
            double d = Math.Sqrt(x * x + y * y) * 6371;

            if (d < distance)
            {
                distance = d;
                result = input[1];
            }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(result);
    }
}