using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Don't let the machines win. You are humanity's last hope...
 **/
class Player
{
    static void Main(string[] args)
    {
        int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
        int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
        List<List<Node>> grid = new List<List<Node>>();
        for (int i = 0; i < height; i++)
        {
            string line = Console.ReadLine(); // width characters, each either 0 or .
            Console.Error.WriteLine(String.Format("Line {0}: {1}", i, line));
            List<Node> row = new List<Node>();
            for (int j = 0; j < width; j++)
            {
                char node = line[j];
                Node n = Node.Empty;
                if (node == '0')
                    n = new Node() { X = j, Y = i };
                row.Add(n);

                if (n.IsEmpty == true)
                    continue;

                if (j > 0)
                {
                    int jj = j - 1;
                    while (jj >= 0)
                    {
                        Node l = row[jj];
                        jj--;
                        if (l.IsEmpty == true)
                            continue;

                        l.LeftNeighbor = n;
                        break;
                    }
                }

                if (i > 0)
                {
                    int ii = i + -1;
                    while (ii >= 0)
                    {
                        Node b = grid[ii][j];
                        ii--;
                        if (b.IsEmpty == true)
                            continue;

                        b.BottomNeighbor = n;
                        break;
                    }
                }
            }
            grid.Add(row);
        }

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Node node = grid[i][j];
                if (node.IsEmpty == true)
                    continue;

                Console.Error.WriteLine(String.Format("Node {0}{2}: {1}", i, node, j));
                Console.WriteLine(node.ToString());
            }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        //Console.WriteLine("0 0 1 0 0 1"); // Three coordinates: a node, its right neighbor, its bottom neighbor
    }

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Node LeftNeighbor { get; set; }
        public Node BottomNeighbor { get; set; }

        public bool IsEmpty { get { return X < 0 && Y < 0; } }

        public static Node Empty = new Node() { X = -1, Y = -1 };

        public Node()
        {
            LeftNeighbor = Node.Empty;
            BottomNeighbor = Node.Empty;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5}", X, Y, LeftNeighbor.X, LeftNeighbor.Y, BottomNeighbor.X, BottomNeighbor.Y);
        }
    }
}