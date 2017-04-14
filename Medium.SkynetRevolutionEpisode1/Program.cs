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
        List<Node> nodes = new List<Node>();
        List<Link> links = new List<Link>();
        int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        int L = int.Parse(inputs[1]); // the number of links
        int E = int.Parse(inputs[2]); // the number of exit gateways

        for (int i = 0; i < N; i++)
            nodes.Add(new Node() { Index = i });

        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
            Node n1 = nodes[N1];
            Node n2 = nodes[N2];
            Link link = new Link() { N1 = n1, N2 = n2 };
            n1.Links.Add(link);
            n2.Links.Add(link);
            links.Add(link);
        }
        for (int i = 0; i < E; i++)
        {
            int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            nodes[EI].IsExit = true;
        }

        for (int i = 0; i < N; i++)
        {
            Node n = nodes[i];
            if (n.IsExit == true || n.IsLinked == false)
                continue;

            foreach (Link link in n.Links)
            {
                Node to = link.N1;
                if (to.Id == n.Id)
                    to = link.N2;

                int stepCount = int.MaxValue; // Crawl(to, n, n);
                if (to.IsExit == true)
                    stepCount = 1;
                n.Paths.Add(new Path() { Link = link, StepCount = stepCount });
            }
        }

        // game loop
        while (true)
        {
            int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Node infected = nodes[SI];
            Link link = infected.Paths.Where(x => x.Link.IsSevered == false).OrderBy(x => x.StepCount).Select(x => x.Link).FirstOrDefault();
            if (link != null)
            {
                link.IsSevered = true;
                Console.WriteLine(link.ToString()); // Example: 0 1 are the indices of the nodes you wish to sever the link between
            }
        }
    }

    static int Crawl(Node current, Node from, Node origin)
    {
        if (origin != null && current.Index == origin.Index)
            return 0;

        if (origin == null)
            origin = current;

        if (current.IsExit == true)
            return 1;

        int result = int.MaxValue;
        foreach (Link link in current.Links)
        {
            Node to = link.N1;
            if (to.Id == current.Id)
                to = link.N2;

            if (to.Id == origin.Id || to.Id == from.Id)
                continue;

            result = Math.Min(result, Crawl(to, current, origin));
        }
        if (result == int.MaxValue)
            return result;

        return result + 1;
    }

    public class Node
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public List<Link> Links { get; set; }

        public bool IsExit { get; set; }
        public bool IsLinked { get { return Links.Any(); } }

        public List<Path> Paths { get; set; }

        public Node()
        {
            Id = Guid.NewGuid();
            Links = new List<Link>();
            Paths = new List<Path>();
        }
    }

    public class Link
    {
        public Guid Id { get; set; }
        public Node N1 { get; set; }
        public Node N2 { get; set; }

        public bool IsSevered { get; set; }

        public Link()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", N1.Index, N2.Index);
        }
    }

    public class Path
    {
        public Guid Id { get; set; }
        public Link Link { get; set; }
        public int StepCount { get; set; }
    }
}