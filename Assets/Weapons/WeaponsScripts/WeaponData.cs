using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Weapon", menuName = "Weapons/New Weapon",  order = 1)]
public class WeaponData : ScriptableObject
{
    public GameObject weaponPrefab;
    public StatsData statsL;
    public StatsData statsR;
    public RuntimeAnimatorController animatorController;
    public float pickupOffest;
    public Mesh model;
    public Material material;
    public Object[] custom;
}
