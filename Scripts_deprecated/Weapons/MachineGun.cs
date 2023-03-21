using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    protected override void LoadData()
    {
        damage = 20;
        timeBetweenShooting = 0.5f;
        spread = 0.01f;
        reloadTime = 1.5f;
        range = 99999f;
        timeBetweenShots = 0.1f;
        magazineSize = 100;
        bulletsPerTap = 1;
        allowButtonHold = true;
        isProjectile = false;
    }
}
