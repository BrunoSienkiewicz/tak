using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    [System.NonSerialized] public List<Upgrade> upgrades = new List<Upgrade>();

    [SerializeField] float _damage;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _cooldown;
    [SerializeField] int _bulletsPerShoot;
    [SerializeField] float _recoil;

    [Header("Ammo")]

    [SerializeField] int _ammoCapacity;
    [SerializeField] float _ammoRegen;

    [Header("Bullet")]

    [SerializeField] float _bulletSpeed;
    [SerializeField] float _bulletSize;
    [SerializeField] int _bulletBounce;
    [SerializeField] int _ammoCost;

    [System.NonSerialized] public float damage;
    [System.NonSerialized] public float attackSpeed;
    [System.NonSerialized] public float cooldown;
    [System.NonSerialized] public int bulletsPerShoot;
    [System.NonSerialized] public float recoil;
    [System.NonSerialized] public int ammoCapacity;
    [System.NonSerialized] public float ammoRegen;
    [System.NonSerialized] public float bulletSpeed;
    [System.NonSerialized] public float bulletSize;
    [System.NonSerialized] public int bulletBounce;
    [System.NonSerialized] public int ammoCost;

    public void ApplyData(StatsData statsData)
    {
        _damage = statsData.damage;
        _attackSpeed = statsData.attackSpeed;
        _cooldown = statsData.cooldown;
        _bulletsPerShoot = statsData.bulletsPerShoot;
        _recoil = statsData.recoil;

        _ammoCapacity = statsData.ammoCapacity;
        _ammoRegen = statsData.ammoRegen;

        _bulletSpeed = statsData.bulletSpeed;
        _bulletSize = statsData.bulletSize;
        _bulletBounce = statsData.bulletBounce;

        _ammoCost = statsData.ammoCost;
    }

    public Stats()
    {

    }

    public Stats(StatsData statsData)
    {
        ApplyData(statsData);
    }
    
    public virtual void UpdateUpgrades()
    {
        damage = _damage;
        attackSpeed = _attackSpeed;
        cooldown = _cooldown;
        bulletsPerShoot = _bulletsPerShoot;
        recoil = _recoil;

        ammoCapacity = _ammoCapacity;
        ammoRegen = _ammoRegen;
        
        bulletSpeed = _bulletSpeed;
        bulletSize = _bulletSize;
        bulletBounce = _bulletBounce;
        ammoCost = _ammoCost;


        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.ApplyUpgrade(this);
        }
    }

    public virtual void AddUpgrade(Upgrade upgrade)
    {
        upgrades.Add(upgrade);
        UpdateUpgrades();
    }

    public virtual void RemoveUpgrade(Upgrade upgrade)
    {
        upgrades.Remove(upgrade);
        UpdateUpgrades();
    }
}
