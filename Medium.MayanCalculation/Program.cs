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
    const double NUMERAL_COUNT = 20;

    static int H = 0;
    static int L = 0;
    static List<string> numerals = new List<string>();
    static Dictionary<string, Func<double, double, double>> operations = new Dictionary<string, Func<double, double, double>>()
    {
        { "*", (x, y) => x * y }, { "/", (x, y) => x / y }, { "+", (x, y) => x + y }, { "-", (x, y) => x - y }
    };

    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        L = int.Parse(inputs[0]);
        H = int.Parse(inputs[1]);

        for (int i = 0; i < NUMERAL_COUNT; i++)
            numerals.Add("");

        // popule numerals lines
        for (int i = 0; i < H; i++)
        {
            string line = Console.ReadLine();
            for (int j = 0; j < NUMERAL_COUNT; j++)
            {
                numerals[j] += line.Substring(0, L);
                line = line.Substring(L);
            }
        }

        double first = FetchNumber(int.Parse(Console.ReadLine()));
        double second = FetchNumber(int.Parse(Console.ReadLine()));
        double result = operations[Console.ReadLine()](first, second);
        List<string> results = Decompose(result);

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        foreach (string numeral in results)
        {
            string line = numeral;
            for (int j = 0; j < H; j++)
            {
                Console.WriteLine(line.Substring(0, L));
                line = line.Substring(L);
            }
        }
    }

    private static double FetchNumber(int lines)
    {
        int numeralCount = lines / H;
        double result = 0;
        for (int i = 0; i < numeralCount; i++)
        {
            string numeral = "";
            for (int j = 0; j < H; j++)
                numeral += Console.ReadLine();
            double value = numerals.IndexOf(numeral);
            double power = numeralCount - 1 - i;
            result += value * Math.Pow(NUMERAL_COUNT, power);
        }
        return result;
    }

    private static List<string> Decompose(double number)
    {
        List<string> list = new List<string>();
        int power = 0;
        for (int i = 0; i < 100; i++)
        {
            if (number < Math.Pow(NUMERAL_COUNT, i))
                break;
            power = i;
        }

        while (power >= 0)
        {
            double divider = Math.Pow(NUMERAL_COUNT, power);
            double modulo = number % divider;
            double value = (number - modulo) / divider;

            list.Add(numerals[(int)value]);
            power--;
            number = modulo;
        }

        return list;
    }
}