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
    static Dictionary<int, List<string>> _dictionary = new Dictionary<int, List<string>>()
    {
        { 1, new List<string>() },{ 2, new List<string>() },{ 3, new List<string>() },{ 4, new List<string>() },{ 5, new List<string>() },{ 6, new List<string>() },{ 7, new List<string>() }
    };

    static void Main(string[] args)
    {
        int wordCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < wordCount; i++)
        {
            string word = Console.ReadLine();
            if (word.Length > 7)
                continue;

            _dictionary[word.Length].Add(word);
        }

        string letters = Console.ReadLine();
        letters = String.Join("", letters.OrderBy(x => x));

        List<string> results = new List<string>();
        for(int i = 7; i > 0; i--)
        {
            List<string> dictionary = _dictionary[i];
            foreach(string word in dictionary)
            {
                string copy = word.Clone() as string;
                foreach(char letter in letters)
                {
                    int idx = copy.IndexOf(letter);
                    if (idx < 0)
                        continue;
                    else
                        copy = copy.Remove(idx, 1);
                }
                if (copy.Length == 0)
                    results.Add(word);
            }
        }

        string result = results.OrderByDescending(x => PointFor(x)).FirstOrDefault();

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(result);
    }

    static int PointFor(string word)
    {
        return word.Sum(x => PointFor(x));
    }

    static int PointFor(char letter)
    {
        switch(letter)
        {
            case 'd':
            case 'g': return 2;
            case 'b':
            case 'c':
            case 'm':
            case 'p': return 3;
            case 'f':
            case 'h':
            case 'v':
            case 'w':
            case 'y': return 4;
            case 'k': return 5;
            case 'j':
            case 'x': return 8;
            case 'q':
            case 'z': return 10;
        }
        return 1;
    }
}