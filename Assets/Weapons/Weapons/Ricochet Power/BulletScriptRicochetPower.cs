using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScriptRicochetPower : GenericBullet
{
    private Vector3 _lastVelocity;
    public float damageIncrease;

    protected override void BounceOffWall(Collision coll, Vector3 lastVelocity, Rigidbody rb)
    {
        damage += damageIncrease;
        base.BounceOffWall(coll, lastVelocity, rb);
    }
}
