using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Constraints
{
    public Ammo ammo;
    public Stats stats;
    public Downtime downtime;
    // TODO: cooldowny
    public abstract void Use();
    public abstract bool ReadyToUse();
}
