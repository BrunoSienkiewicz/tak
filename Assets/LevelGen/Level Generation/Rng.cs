using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
//using UnityEngine;

public class Rng : System.Random
{
    int seed;
    public Rng(int seed) : base(seed)
    {
        this.seed = seed;
        //Debug.Log(seed);
    }

    public Rng() : base(Environment.TickCount)
    {
        this.seed =  Environment.TickCount; 
    }

    public bool NextBool()
    {
        return this.Next(0,2)==1;
    }

    public T WeightedRandom<T>(List<(T,int)> desc)
    {
        desc = desc.OrderByDescending(x => x.Item2).ToList();
        int totalWeight = desc.Sum(x => x.Item2);
        int rand = this.Next(0,totalWeight);
        foreach (var trait in desc)
        {
            if (rand<trait.Item2)
            {
                return trait.Item1;
            }
            rand -= trait.Item2;
        }
        return desc[0].Item1;
    }
}