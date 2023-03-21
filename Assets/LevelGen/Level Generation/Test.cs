using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
public class Test : MonoBehaviour
{
    Graph graph;
    Rng random;
    public Test()
    {
        graph = new Graph();
        random = new Rng();
    } 
    void Start()
    {
        Debug.Log("QWERTY");
        graph.Generate(10,15);
        foreach (var i in graph.nodes)
        {
            Debug.Log(i);
        }
        /*
        List<(string,int)> list = new List<(string, int)>();
        list.Add(new ValueTuple<string,int>("A",500));
        list.Add(new ValueTuple<string,int>("B",100));
        list.Add(new ValueTuple<string,int>("C",100));
        list.Add(new ValueTuple<string,int>("D",500));
        Dictionary<string,int> dict = new Dictionary<string, int>();
        dict.Add("A",0);
        dict.Add("B",0);
        dict.Add("C",0);
        dict.Add("D",0);
        for (int i = 0; i<1000; i++)
        {
            dict[random.WeightedRandom<string>(list)] += 1;
        }
        foreach (var curr in dict)
        {
            Debug.Log($"{curr.Key}: {curr.Value}");
        }
        */
    }
}