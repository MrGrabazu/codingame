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
        int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
        Queue<int> player1 = new Queue<int>();
        for (int i = 0; i < n; i++)
            player1.Enqueue(GetCardValue(Console.ReadLine())); // the n cards of player 1

        int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
        Queue<int> player2 = new Queue<int>();
        for (int i = 0; i < m; i++)
            player2.Enqueue(GetCardValue(Console.ReadLine())); // the m cards of player 2

        int playCount = 0;
        Queue<int> play1 = new Queue<int>();
        Queue<int> play2 = new Queue<int>();
        while (player1.Any() == true && player2.Any() == true)
        {
            int card1 = player1.Dequeue();
            int card2 = player2.Dequeue();

            play1.Enqueue(card1);
            play2.Enqueue(card2);

            if (card1 == card2)
            {
                if (player1.Count <= 3 || player2.Count <= 3)
                    break;
                for (int i = 0; i < 3; i++)
                {
                    play1.Enqueue(player1.Dequeue());
                    play2.Enqueue(player2.Dequeue());
                }
                continue;
            }
            else
                EnqueueGame(card1 > card2 ? player1 : player2, play1, play2);

            playCount++;
        }

        if (player1.Any() == false && player2.Any() == true)
            Console.WriteLine("2 {0}", playCount);
        else if (player1.Any() == true && player2.Any() == false)
            Console.WriteLine("1 {0}", playCount);
        else
            Console.WriteLine("PAT");
        Console.ReadLine();
    }

    static Dictionary<string, int> _headValues = new Dictionary<string, int>() { { "J", 11 }, { "Q", 12 }, { "K", 13 }, { "A", 14 } };
    static int GetCardValue(string card)
    {
        string value = card.TrimEnd('D', 'H', 'C', 'S');
        int result = 0;
        if (int.TryParse(value, out result) == true)
            return result;
        return _headValues[value.ToUpper()];
    }

    static void EnqueueGame(Queue<int> winner, Queue<int> game1, Queue<int> game2)
    {
        while (game1.Any() == true) winner.Enqueue(game1.Dequeue());
        while (game2.Any() == true) winner.Enqueue(game2.Dequeue());
    }
}