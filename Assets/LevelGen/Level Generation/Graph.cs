using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Node
{
    public int id;
    RoomTrait characteristics;
    public int startDistance;
    public int indegree;
    List<Node> connections;
    public int available => indegree-connections.Count;
    public Node(int id, RoomTrait charac, int distance, int indegree, Node anchor = null)
    {
        this.id = id;
        this.characteristics = charac;
        this.startDistance = distance;
        this.indegree = indegree;
        this.connections = new List<Node>();
        if (anchor!=null) Connect(this,anchor);
    }
    public static bool Connected(Node a, Node b) => a.connections.Contains(b);
    public static void Connect(Node a, Node b)
    {
        if (a.available<=0 || b.available<=0) throw new InvalidOperationException("Node can't create more connections!");
        if (a.connections.Contains(b)) throw new InvalidOperationException("Nodes are already connected!");
        a.connections.Add(b);
        b.connections.Add(a);
    }

    public override string ToString()
    {
        string result = $"{id}<{Enum.GetName(typeof(RoomTrait),characteristics)},{indegree}>: ";
        foreach (var i in connections)
        {
            result+=i.id+" ";
        }
        return result;
    }
    public void Dispose()
    {
        connections.Clear();
    }
}

class Graph
{
    int currID = 1;
    Rng rng = new Rng(1);
    public List<Node> nodes = new List<Node>();
    private List<(RoomTrait,int)> probabilities = new List<(RoomTrait, int)>();

    public Graph()
    {
        probabilities.Add(new ValueTuple<RoomTrait,int>(RoomTrait.Battle,1100));
        probabilities.Add(new ValueTuple<RoomTrait,int>(RoomTrait.Event,100));
        probabilities.Add(new ValueTuple<RoomTrait,int>(RoomTrait.Treasure,400));
        probabilities.Add(new ValueTuple<RoomTrait,int>(RoomTrait.Shop,400));
    }
    RoomTrait GetRandomTrait(RoomTrait possible)
    {
        var options = possible.GetFlags().ToList();
        options.RemoveAll(trait => trait==RoomTrait.None | trait==RoomTrait.Action | trait==RoomTrait.All);
        var result = options[rng.Next(options.Count)];
        //Debug.Log(options.Contains(RoomTrait.None));
        return result;
    }
    RoomTrait GetRandomTrait(List<(RoomTrait,int)> desc) => rng.WeightedRandom<RoomTrait>(desc);
    Node GetRandomisedNode(int minIndegree, int maxIndegree, int distance, Node anchor = null)
    {
        RoomTrait trait = GetRandomTrait(probabilities);

        return new Node(currID++,trait,distance,rng.Next(minIndegree,maxIndegree+1),anchor);
    }

    void FillChildren(Node startingPoint, int leaveOpen, int depth)
    {
        if (depth==0) return; 
        //if (startingPoint.indegree<=2) leaveOpen = leaveOpen==2 ? 0 : 1; 
        while (startingPoint.available>leaveOpen)
        {
            Node curr = GetRandomisedNode((startingPoint.startDistance>4 ? 1 : 2),(startingPoint.startDistance>4 ? 2 : 3),startingPoint.startDistance+1,startingPoint);
            nodes.Add(curr);
            FillChildren(curr,rng.Next(0,0),depth-1);
        }
    } 
    void ConnectExistingNodes()
    {
        Node a,b;
        a = new Node(currID++,RoomTrait.Exit,0,1);
        nodes.Add(a);
        var toBeConnected = nodes.Where(node => node.available>0);
        while (toBeConnected.Count()>1)
        {
            a = toBeConnected.ElementAt(rng.Next(toBeConnected.Count()));
            do
            {
                b = toBeConnected.ElementAt(rng.Next(toBeConnected.Count()));
            }
            while (a==b || Node.Connected(a,b));
            Node.Connect(a,b);
            toBeConnected = nodes.Where(node => node.available>0);
        }
        if (toBeConnected.Count()>0)
        {
            nodes.Add(GetRandomisedNode(1,1,toBeConnected.First().startDistance+1,toBeConnected.First()));
        }
    }
    public void Create()
    {
        nodes.Add(new Node(currID++,RoomTrait.Entrance,0,1));
        FillChildren(nodes[0],0,5);
        ConnectExistingNodes();
    }
    public void Trash()
    {
        foreach (var i in nodes)
        {
            i.Dispose();
        }
        nodes.Clear();
        currID = 1;
    }

    public void Generate(int minimumRoomCount, int maximumRoomCount)
    {
        bool satisfied = false;
        Create();
        while (!satisfied)
        {
            Trash();
            Create();
            if (currID>=minimumRoomCount && currID<=maximumRoomCount)
            {
                satisfied=true;
            }
        }
    }
}