using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTestWeaponFactory : WeaponFactoryMethod
{
    public GameObject weapon = GameObject.Find("Weapon");
    public override Weapon CreateWeapon(GameObject target)
    {
        target = weapon;
        Weapon projectileTest = null;
        projectileTest = target.AddComponent<ProjectileTest>();
        return projectileTest;
    }
}
