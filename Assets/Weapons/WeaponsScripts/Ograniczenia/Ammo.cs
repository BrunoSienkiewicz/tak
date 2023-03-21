using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    public Stats stats;
    public bool readyToUse;
    public abstract void Use(int amount);
}
