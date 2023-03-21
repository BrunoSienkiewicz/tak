using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
///Use this class to define for how many physics steps action should take place. If applicable, store inside <see cref="DurationCollection"/>
///</summary>
public class Duration : IDisposable
{
    private int totalDuration;
    public int elapsed;

    public bool completed {
        get {return elapsed >= totalDuration;}
        set {
            switch (value)
            {
                case true:
                    elapsed = totalDuration;
                    break;
                case false:
                    throw new System.InvalidOperationException("Undefined behaviour");
            }
        }
    }

    private System.Action runtime;
    private System.Action final;
    
    public Duration(int duration, System.Action runtimeFunction = null, System.Action finalFunction = null)
    {
        this.totalDuration = duration;
        this.runtime = runtimeFunction;
        this.final = finalFunction;
    }
    public void Dispose()
    {
        this.runtime = null;
        this.final = null;
    }
    public bool Update()
    {
        elapsed++;
        if (completed)
        {
            if (this.final!=null)
                this.final();
            return true;
        }
        else if (this.runtime!=null)
            this.runtime();
        return false;
    }
}

///<summary>Collection of Duration instances, extending their behaviour and improving readability.</summary>
public class DurationCollection {
    public Dictionary<String,Duration> TO_ADD = new Dictionary<String,Duration>();
    public Dictionary<String,Duration> ALL = new Dictionary<String,Duration>();
    public List<String> GC = new List<String>();
    public Duration this[String key]
   {
      get {
        if(ALL.ContainsKey(key))
                return ALL[key];
        return null;
        }
      set => this.Add(key,value);
   }
    public DurationCollection()
    {}

    public void Update()
    {
        foreach(var dur in TO_ADD)
        {
            if (ALL.ContainsKey(dur.Key))
                ALL[dur.Key].Dispose();
            ALL[dur.Key] = dur.Value;
        }
        TO_ADD.Clear();
        foreach(var dur in ALL)
        {
            if (dur.Value.Update())
            {
                GC.Add(dur.Key);
            }
        }
        foreach(var dur in GC)
        {
            ALL[dur].Dispose();
            ALL.Remove(dur);
        }
        GC.Clear();
    }
    public void Add(String name, Duration duration)
    {
        TO_ADD[name]=duration;
    }
    public void Add(String name, int duration, System.Action runtimeFunction = null,System.Action finalFunction = null)
    {
        TO_ADD[name]=new Duration(duration,runtimeFunction,finalFunction);
    }
    public void Complete(String name)
    {
        if(ALL.ContainsKey(name))
            ALL[name].completed = true;
    }

}