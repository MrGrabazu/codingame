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
        string[] inputs = Console.ReadLine().Split(' ');
        int height = int.Parse(inputs[0]);
        int width = int.Parse(inputs[1]);

        List<List<ISlot>> map = new List<List<ISlot>>();
        int x = 0, y = 0;
        SlotFactory factory = new SlotFactory();
        for (int o = 0; o < height; o++)
        {
            string row = Console.ReadLine();
            int idx = row.IndexOf("@");
            if (idx >= 0)
            {
                x = idx;
                y = o;
            }

            map.Add(row.ToCharArray().Select((c, a) => factory.GetSlot(c, a, o)).ToList());
        }
        Bender bender = new Bender(map, x, y);
        bender.GoToHell();
    }
}

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Bender
{
    public List<string> Path { get; set; }
    public Coordinate Position { get; set; }
    public bool BreakerMode { get; set; }
    public List<string> Priorities { get; set; }
    public List<List<ISlot>> Map { get; set; }
    public bool Arrived { get; set; }
    public string CurrentDirection { get; set; }
    public string CurrentOpposite { get; set; }

    public Bender(List<List<ISlot>> map, int x, int y)
    {
        Map = map;
        Position = new Coordinate() { X = x, Y = y };
        Path = new List<string>();
        Priorities = new List<string>() { Direction.SOUTH, Direction.EAST, Direction.NORTH, Direction.WEST };
        CurrentDirection = Direction.SOUTH;
        CurrentOpposite = Direction.NORTH;
    }

    public void GoToHell()
    {
        int i = 0;
        while(Arrived == false && i < 1000)
        {
            i++;
            ISlot slot = Next(CurrentDirection);
            if (slot.IsAccessible(this) == true)
            {
                Go(CurrentDirection);
                slot.Affect(this);
                continue;
            }

            foreach (string direction in Priorities)
            {
                if (direction == CurrentDirection)
                    continue;

                slot = Next(direction);
                if (slot.IsAccessible(this) == true)
                {
                    Go(direction);
                    slot.Affect(this);
                    break;
                }
            }
        }
        if (Arrived == false)
            Console.WriteLine("LOOP");
    }

    public void Go(string direction)
    {
        switch (direction)
        {
            case Direction.SOUTH: Position.Y++; CurrentOpposite = Direction.NORTH; break ;
            case Direction.NORTH: Position.Y--; CurrentOpposite = Direction.SOUTH; break;
            case Direction.EAST: Position.X++; CurrentOpposite = Direction.WEST; break;
            case Direction.WEST: Position.X--; CurrentOpposite = Direction.EAST; break;
        }
        CurrentDirection = direction;
        Path.Add(direction);
    }

    public ISlot Next(string direction)
    {
        int x = Position.X;
        int y = Position.Y;
        switch (direction)
        {
            case Direction.SOUTH: y++; break;
            case Direction.NORTH: y--; break;
            case Direction.EAST: x++; break;
            case Direction.WEST: x--; break;
        }
        return Map[y][x];
    }

    public void Die()
    {
        Arrived = true;
        foreach (string direction in Path)
            Console.WriteLine(direction);
    }
}

public interface ISlot
{
    Coordinate Position { get; set; }
    bool IsAccessible(Bender bender);
    void Affect(Bender bender);
}

public class Slot : ISlot
{
    public Coordinate Position { get; set; }
    public Slot(int x, int y) { Position = new Coordinate() { X = x, Y = y }; }
    public virtual bool IsAccessible(Bender bender) { return true; }
    public virtual void Affect(Bender bender) { }
}

public class Teleporter : Slot
{
    public Teleporter Destination { get; set; }
    public Teleporter(int x, int y) : base(x, y) { }
    public override void Affect(Bender bender) { bender.Position.X = Destination.Position.X; bender.Position.Y = Destination.Position.Y; }
}

public class Wall : Slot
{
    public Wall(int x, int y) : base(x, y) { }
    public override bool IsAccessible(Bender bender) { return false; }
}

public class Obstacle : Slot
{
    public Obstacle(int x, int y) : base(x, y) { }
    public override bool IsAccessible(Bender bender) { return bender.BreakerMode == true; }
    public override void Affect(Bender bender) { bender.Map[Position.Y][Position.X] = new Slot(Position.X, Position.Y); }
}

public class Modifier : Slot
{
    public string Direction { get; set; }
    public Modifier(string direction, int x, int y) : base(x, y) { Direction = direction; }
    public override void Affect(Bender bender) { bender.CurrentDirection = Direction; bender.CurrentOpposite = ""; }
}

public class Inverter : Slot
{
    public Inverter(int x, int y) : base(x, y) { }
    public override void Affect(Bender bender) { bender.Priorities.Reverse(); }
}

public class Beer : Slot
{
    public Beer(int x, int y) : base(x, y) { }
    public override void Affect(Bender bender) { bender.BreakerMode = !bender.BreakerMode; }
}

public class End : Slot
{
    public End(int x, int y) : base(x, y) { }
    public override void Affect(Bender bender) { bender.Die(); }
}

public class SlotFactory
{
    Teleporter first = null;

    public ISlot GetSlot(char slot, int x, int y)
    {
        switch(slot)
        {
            case '@': return new Slot(x, y);
            case '#': return new Wall(x, y);
            case 'X': return new Obstacle(x, y);
            case 'I': return new Inverter(x, y);
            case 'B': return new Beer(x, y);
            case 'S': return new Modifier(Direction.SOUTH, x, y);
            case 'E': return new Modifier(Direction.EAST, x, y);
            case 'N': return new Modifier(Direction.NORTH, x, y);
            case 'W': return new Modifier(Direction.WEST, x, y);
            case 'T':
            {
                if (first == null)
                {
                    first = new Teleporter(x, y);
                    return first;
                }
                else
                {
                    Teleporter second = new Teleporter(x, y);
                    second.Destination = first;
                    first.Destination = second;
                    return second;
                }
            }
            case '$': return new End(x, y);
        }
        return new Slot(x, y);
    }
}

public class Direction
{
    public const string SOUTH = "SOUTH";
    public const string NORTH = "NORTH";
    public const string EAST = "EAST";
    public const string WEST = "WEST";
}