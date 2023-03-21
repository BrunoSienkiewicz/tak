using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public WeaponData[] weapons;
    public LayerMask WhatIsWeapon;

    public void SpawnWeapon(WeaponData weaponData, Vector3 Position)
    {
        GameObject newWeapon = Instantiate(weaponData.weaponPrefab, Position, new Quaternion(0, 0, 0, 0));
        newWeapon.GetComponent<AWeapon>().Create(weaponData);
        newWeapon.layer = FindLayerIndex(WhatIsWeapon);
        newWeapon.name = weaponData.name;
    }

    private int FindLayerIndex(int n)
    {
        if (n == 0)
        {
            throw new System.InvalidOperationException("Please set weapon layer in WeaponSpawner");
        }
        int ret = 0;
        while (n % 2 == 0)
        {
            n /= 2;
            ret++;
        }
        if(n!=1)
        {
            throw new System.InvalidOperationException("bron moze byc jednym layerem tlumoku");
        }
        return ret;
    }

    // Start is called before the first frame update
    void Start()
    {
        int j = 0;
        foreach (WeaponData i in weapons)
        {
            SpawnWeapon(i, transform.position + new Vector3(0, (j+2)*3, 0));
            j++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
