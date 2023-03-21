using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardConstraints : Constraints
{
    public override void Use()
    {
        if (ammo != null)
        {
            ammo.Use(stats.ammoCost);
        }
        downtime.Use(stats.attackSpeed);
    }

    public override bool ReadyToUse()
    {
        if(( ammo != null || ammo.readyToUse) && downtime.ReadyToUse())
        {
            return true;
        }
        return false;
    }
}
