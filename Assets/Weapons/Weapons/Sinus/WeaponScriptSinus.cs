using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponScriptSinus: AWeapon
{
    protected override void ShootingInput()
    {
        if (GI.attackL)
        {
            ShootL();
        }
        if (GI.attackRDown)
        {
            ShootR();
        }
    }

    public override void Create(WeaponData weaponData)
    {
        Transform baseShootPoint = Instantiate(new GameObject(), transform).transform;
        Transform HighShootPoint = Instantiate(new GameObject(), transform).transform;
        Transform LowShootPoint = Instantiate(new GameObject(), transform).transform;
        baseShootPoint.localPosition += new Vector3(0, 0, 1.25f);
        HighShootPoint.localPosition += new Vector3(0, 1, 1.25f);
        LowShootPoint.localPosition += new Vector3(0, -1, 1.25f);

        actionsL.Add(new ShootBullet((GameObject)weaponData.custom[0], HighShootPoint, transform));
        actionsL.Add(new ShootBullet((GameObject)weaponData.custom[0], LowShootPoint, transform));
        actionsR.Add(new ShootBullet((GameObject)weaponData.custom[1], baseShootPoint, transform));

        ammoL = gameObject.AddComponent<StandardAmmo>();

        base.Create(weaponData);
    }

    protected override void Setup()
    {
        base.Setup();

        constraintsR.ammo = ammoL;
    }

}
