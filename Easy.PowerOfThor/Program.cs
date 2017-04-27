using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 * ---
 * Hint: You can use the debug stream to print initialTX and initialTY, if Thor seems not follow your orders.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int lightX = int.Parse(inputs[0]); // the X position of the light of power
        int lightY = int.Parse(inputs[1]); // the Y position of the light of power
        int initialTX = int.Parse(inputs[2]); // Thor's starting X position
        int initialTY = int.Parse(inputs[3]); // Thor's starting Y position
        int thorX = initialTX;
        int thorY = initialTY;

        // game loop
        while (true)
        {
            int remainingTurns = int.Parse(Console.ReadLine());

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            string directionX = "";
            if (lightX < thorX)
            {
                directionX = "W";
                thorX++;
            }
            else if (lightX > thorX)
            {
                directionX = "E";
                thorX--;
            }

            string directionY = "";
            if (lightY < thorY)
            {
                directionY = "N";
                thorY--;
            }
            else if (lightY > thorY)
            {
                directionY = "S";
                thorY++;
            }

            Console.WriteLine(String.Format("{0}{1}", directionY, directionX)); // A single line providing the move to be made: N NE E SE S SW W or NW
        }
    }
}