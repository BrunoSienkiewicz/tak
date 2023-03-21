using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ograniczenia
{
    public Stats stats;
    public bool readyToShoot;
    public abstract void Update();
    public abstract void Use(int amount);
}
