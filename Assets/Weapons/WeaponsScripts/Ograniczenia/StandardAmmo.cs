using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAmmo : Ammo
{
    public bool overheat = false;
    public float Ammo;

    private void Update()
    {
        Overheat();
        AmmoRegeneration();
    }

    protected void AmmoRegeneration()
    {
        Ammo = Mathf.Clamp(Ammo + stats.ammoRegen * Time.deltaTime, 0, stats.ammoCapacity);
    }

    protected void Overheat()
    {
        if (Ammo <= 0 && !overheat)
        {
            Ammo = 0;
            overheat = true;
            readyToUse = false;
        }
        if (Ammo == stats.ammoCapacity)
        {
            overheat = false;
            readyToUse = true;
        }
    }

    public override void Use(int amount)
    {
        Ammo -= amount;
    }
}
