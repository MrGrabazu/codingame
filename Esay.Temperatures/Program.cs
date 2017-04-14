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
        int N = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
        string TEMPS = Console.ReadLine(); // the N temperatures expressed as integers ranging from -273 to 5526

        List<Temperature> temperatures = new List<Temperature>();
        string[] splits = TEMPS.Split(' ');
        foreach (string split in splits)
        {
            int value = 0;
            if (Int32.TryParse(split, out value) == false)
                continue;
            temperatures.Add(new Temperature(value));
        }

        if(temperatures.Any() == false)
        {
            Console.WriteLine("0");
            return;
        }

        Temperature closest = new Temperature(Int32.MaxValue);
        foreach(Temperature temperature in temperatures)
        {
            if (closest.PositiveValue > temperature.PositiveValue)
                closest = temperature;
            else if (closest.PositiveValue == temperature.PositiveValue && closest.IsNegative == true && temperature.IsNegative == false)
                closest = temperature;
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(closest.Value);
    }

    public class Temperature
    {
        public int Value { get; set; }
        public int PositiveValue { get; set; }
        public bool IsNegative { get; set; }

        public Temperature(int value)
        {
            Value = PositiveValue = value;
            if (value < 0)
            {
                IsNegative = true;
                PositiveValue = value * (-1);
            }
        }
    }
}
