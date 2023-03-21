using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : Weapon
{
    protected override void LoadData()
    {
        damage = 20;
        timeBetweenShooting = 0.5f;
        bulletSpeed = 5f;
        reloadTime = 1.5f;
        timeBetweenShots = 0.1f;
        magazineSize = 100;
        bulletsPerTap = 1;
        allowButtonHold = true;
        isProjectile = true;
    }
}
