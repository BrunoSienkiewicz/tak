using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticSniperRifle : AWeapon
{
    protected override void ShootingInput()
    {
        if (GI.attackLDown)
        {
            ShootL();
        }
        if (GI.attackR)
        {
            ShootR();
        }
    }

    public override void Create(WeaponData weaponData)
    {
        Transform ShootPoint = Instantiate(new GameObject(), transform).transform;
        ShootPoint.localPosition += new Vector3(0, 0, 1.25f);

        actionsR.Add(new ShootBullet((GameObject)weaponData.custom[0], ShootPoint, transform));
        actionsR.Add(new ShootBullet((GameObject)weaponData.custom[0], ShootPoint, transform));

        ammoL = gameObject.AddComponent<StandardAmmo>();

        base.Create(weaponData);
    }

    protected override void Setup()
    {
        base.Setup();

        constraintsR.ammo = ammoL;
    }
}
