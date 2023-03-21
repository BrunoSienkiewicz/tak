using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downtime
{
    private float readyToUseTime = 0;
    public void Use(float time)
    {
        readyToUseTime = Time.time + time;
    }

    public bool ReadyToUse()
    {
        if(Time.time >= readyToUseTime)
        {
            return true;
        }
        return false;
    }
}
