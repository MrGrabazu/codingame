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
class Player
{
    static void Main(string[] args)
    {
        int road = int.Parse(Console.ReadLine()); // the length of the road before the gap.
        int gap = int.Parse(Console.ReadLine()); // the length of the gap.
        int platform = int.Parse(Console.ReadLine()); // the length of the landing platform.
        int speedToJump = gap + 1;
        bool hasJump = false;

        // game loop
        while (true)
        {
            int speed = int.Parse(Console.ReadLine()); // the motorbike's speed.
            int coordX = int.Parse(Console.ReadLine()); // the position on the road of the motorbike.

            int length = road - coordX - 1;

            if (hasJump == true || speed > speedToJump)
                Console.WriteLine("SLOW");
            else if (length == 0)
            {
                Console.WriteLine("JUMP");
                hasJump = true;
            }
            else if (speed == speedToJump)
                Console.WriteLine("WAIT");
            else if (speed == speedToJump - 1 && (length % speedToJump) == 0)
                Console.WriteLine("SPEED");
            else if (speed == speedToJump - 1 && (length % speedToJump) > 0)
                Console.WriteLine("WAIT");
            else
                Console.WriteLine("SPEED");
        }
    }
}