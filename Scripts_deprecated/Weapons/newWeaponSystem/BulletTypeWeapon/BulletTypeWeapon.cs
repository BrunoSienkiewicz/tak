using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTypeWeapon : NewWeapon
{
    [Header("Weapon")]

    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject weaponModel;

    [Header("Bullet")]

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float damage = 5f;
    [SerializeField,Min(0f)] float bulletSpeed = 20f;

    [Header("Shooting")]

    [SerializeField, Min(0.001f)] public float shootDelay = 0.1f;
    [SerializeField, Min(0f)] public float recoil = 0f;
    [SerializeField, Min(1)] public int bulletsPerShoot = 1;
    [SerializeField] public bool automatic = true;

    [Header("Ammunition")]

    [SerializeField, Min(1)] int maxAmmunition = 10;
    [SerializeField, Min(0f)] float ammoRegen = 2f;

    [Header("InGameInfo")]

    public float Ammo;
    public bool overheat = false;
    public Transform playerTransform;
    [HideInInspector] public float readyToUseTime;
    [HideInInspector] public float lastShootInput;

    private void Start()
    {
        Ammo = maxAmmunition;
        //Use();
    }

    private void Update()
    {
        AmmoUpdate();
        if (currentState == WeaponState.InUse)
        {
            WeaponInUse();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            Throw();
        }

        /*if(Input.GetKeyDown(KeyCode.K))
        {
            Hide();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Use();
        }
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }*/

    }

    public virtual void AmmoUpdate()
    {
        if (Ammo < 1)
        {
            overheat = true;
        }
        if (readyToUseTime <= Time.time)
        {
            Ammo = Mathf.Clamp(Ammo + ammoRegen * Time.deltaTime, 0, maxAmmunition);
        }
        if (Ammo == maxAmmunition)
        {
            overheat = false;
        }
    }

    public virtual void WeaponInUse()
    {
        if (readyToUseTime <= Time.time && !overheat)
        {
            if (automatic)
            {
                if (Input.GetAxisRaw("Shoot") == 1)
                {
                    Shoot();
                    readyToUseTime = Time.time + shootDelay;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Shoot") == 1 && lastShootInput == 0)
                {
                    Shoot();
                    readyToUseTime = Time.time + shootDelay;
                }
            }
        }
        lastShootInput = Input.GetAxisRaw("Shoot");
    }

    public virtual void Shoot()
    {
        for (int i = 0; i < bulletsPerShoot; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, transform.rotation);

            bullet.transform.RotateAround(playerTransform.position, transform.up, Random.Range(-recoil, recoil));
            bullet.transform.RotateAround(playerTransform.position, transform.forward, Random.Range(0, 360));

            BulletTemplateScript bulletTS = bullet.GetComponent<BulletTemplateScript>();
            bulletTS.speed = bulletSpeed;
            bulletTS.damage = damage;
        }
        Ammo--;
    }
}
