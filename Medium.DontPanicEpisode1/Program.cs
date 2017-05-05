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
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int nbFloors = int.Parse(inputs[0]); // number of floors
        int width = int.Parse(inputs[1]); // width of the area
        int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
        int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
        int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
        int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
        int nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
        int nbElevators = int.Parse(inputs[7]); // number of elevators

        List<Floor> floors = new List<Floor>();
        for (int i = 0; i < nbFloors; i++)
            floors.Add(new Floor() { Number = i, ExitPosition = i == exitFloor ? exitPos : -1 });

        for (int i = 0; i < nbElevators; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int floor = int.Parse(inputs[0]); // floor on which this elevator is found
            int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
            floors[floor].Elevators.Add(new Elevator() { Position = elevatorPos });
        }

        // game loop
        while (true)
        {
            string line = Console.ReadLine();
            Console.Error.WriteLine(line);
            if (line == "-1 -1 NONE")
                Console.WriteLine("WAIT");
            else
            {
                inputs = line.Split(' ');
                Console.WriteLine(floors[int.Parse(inputs[0])].GetOrder(int.Parse(inputs[1]), inputs[2])); // action: WAIT or BLOCK
            }
        }
    }
}

public class Floor
{
    public int Number { get; set; }
    public List<Elevator> Elevators { get; set; }
    public int ExitPosition { get; set; }
    public Floor()
    {
        Elevators = new List<Elevator>();
    }
    public string GetOrder(int position, string direction)
    {
        int positionToReach = ExitPosition;
        if(positionToReach == -1)
        {
            Elevator finalElevator = null;
            if (Elevators.Count == 1)
                finalElevator = Elevators.FirstOrDefault();
            else
            {
                int minDistance = int.MaxValue;
                foreach (Elevator elevator in Elevators)
                {
                    int currentDistance = elevator.Position > position ? elevator.Position - position : position - elevator.Position;
                    if (currentDistance < minDistance)
                    {
                        finalElevator = elevator;
                        minDistance = currentDistance;
                    }
                }
            }

            positionToReach = finalElevator.Position;
        }

        if ((positionToReach < position && direction == Direction.RIGHT) || (positionToReach > position && direction == Direction.LEFT))
            return "BLOCK";
        else
            return "WAIT";
    }
}

public class Direction
{
    public const string RIGHT = "RIGHT";
    public const string LEFT = "LEFT";
}

public class Elevator
{
    public int Position { get; set; }
}