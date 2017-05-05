using System;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int width = int.Parse(inputs[0]);
        int height = int.Parse(inputs[1]);
        List<List<IRoom>> map = new List<List<IRoom>>(height);
        for (int y = 0; y < height; y++)
        {
            List<IRoom> row = new List<IRoom>(width);
            string[] rooms = Console.ReadLine().Split(' '); // represents a line in the grid and contains W integers. Each integer represents one room of a given type.
            for (int x = 0; x < width; x++)
                row.Add(RoomFactory.Get(int.Parse(rooms[x])));
            map.Add(row);
        }
        string exit = Console.ReadLine();
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            IRoom room = map[y][x];
            string exitPoint = room.GetExitPointFor(inputs[2]);
            Console.WriteLine($"{(exitPoint == RoomSide.LEFT ? x - 1 : exitPoint == RoomSide.RIGHT ? x + 1 : x)} {(exitPoint == RoomSide.BOTTOM ? y + 1 : y)}");
        }
    }
}

public class RoomSide
{
    public const string TOP = "TOP";
    public const string BOTTOM = "BOTTOM";
    public const string RIGHT = "RIGHT";
    public const string LEFT = "LEFT";
}

public class RoomFactory
{
    public static IRoom Get(int type)
    {
        switch(type)
        {
            case 1: case 3: case 7: case 8: case 9: case 12: case 13: return new RoomBottomExit();
            case 2: case 6: return new RoomRightOrLeftExit();
            case 10: return new RoomLeftExit();
            case 11: return new RoomRightExit();
            case 4: return new RoomLeftOrBottomExit();
            case 5: return new RoomRightOrBottomExit();
            default: return new RoomBase();
        }
    }
}

public interface IRoom
{
    string GetExitPointFor(string entryPoint);
}

public class RoomBase : IRoom
{
    public virtual string GetExitPointFor(string entryPoint)
    {
        return null;
    }
}

public class RoomBottomExit : RoomBase
{
    public override string GetExitPointFor(string entryPoint)
    {
        return RoomSide.BOTTOM;
    }
}

public class RoomLeftExit : RoomBase
{
    public override string GetExitPointFor(string entryPoint)
    {
        return RoomSide.LEFT;
    }
}

public class RoomRightExit : RoomBase
{
    public override string GetExitPointFor(string entryPoint)
    {
        return RoomSide.RIGHT;
    }
}

public class RoomRightOrLeftExit : RoomBase
{
    public override string GetExitPointFor(string entryPoint)
    {
        if(entryPoint == RoomSide.LEFT) return RoomSide.RIGHT;
        else if(entryPoint == RoomSide.RIGHT) return RoomSide.LEFT;
        return base.GetExitPointFor(entryPoint);
    }
}

public class RoomLeftOrBottomExit : RoomBase
{
    public override string GetExitPointFor(string entryPoint)
    {
        if (entryPoint == RoomSide.TOP) return RoomSide.LEFT;
        else if (entryPoint == RoomSide.RIGHT) return RoomSide.BOTTOM;
        return base.GetExitPointFor(entryPoint);
    }
}

public class RoomRightOrBottomExit : RoomBase
{
    public override string GetExitPointFor(string entryPoint)
    {
        if (entryPoint == RoomSide.TOP) return RoomSide.RIGHT;
        else if (entryPoint == RoomSide.LEFT) return RoomSide.BOTTOM;
        return base.GetExitPointFor(entryPoint);
    }
}