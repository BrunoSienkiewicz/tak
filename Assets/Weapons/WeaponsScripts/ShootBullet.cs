using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : WeaponAction
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public Transform transform;

    public override void Use()
    {
        for (int i = 0; i < stats.bulletsPerShoot; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, shootPoint.position, transform.rotation);

            bullet.transform.RotateAround(transform.position, transform.up, Random.Range(-stats.recoil, stats.recoil));
            bullet.transform.RotateAround(transform.position, transform.forward, Random.Range(0, 360));

            GenericBullet bulletTS = bullet.GetComponent<GenericBullet>();

            bulletTS.speed = stats.bulletSpeed;
            bulletTS.damage = stats.damage;
            bullet.transform.localScale *= stats.bulletSize;
        }
    }

    public ShootBullet(GameObject _bulletPref, Transform _shootPoint, Transform _transform)
    {
        bulletPrefab = _bulletPref;
        shootPoint = _shootPoint;
        transform = _transform;
    }
}
