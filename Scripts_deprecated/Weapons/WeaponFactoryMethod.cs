using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFactoryMethod
{
    //public abstract GameObject weapon;
    public abstract Weapon CreateWeapon(GameObject target);
}
