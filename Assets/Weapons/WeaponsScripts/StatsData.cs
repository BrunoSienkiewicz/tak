using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Weapons/Stats", order = 1)]
public class StatsData : ScriptableObject
{
    [SerializeField] public float damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float cooldown;
    [SerializeField] public int bulletsPerShoot;
    [SerializeField] public float recoil;

    [Header("Ammo")]

    [SerializeField] public int ammoCapacity;
    [SerializeField] public float ammoRegen;

    [Header("Bullet")]

    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletSize;
    [SerializeField] public int bulletBounce;
    [SerializeField] public int ammoCost;
}
