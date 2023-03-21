using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunWeaponFactory : WeaponFactoryMethod
{
    public GameObject weapon = GameObject.Find("Weapon");
    public override Weapon CreateWeapon(GameObject target)
    {
        target = weapon;
        Weapon machineGun = null;
        machineGun = target.AddComponent<MachineGun>();
        return machineGun;
    }
}
