using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] public Transform camTransform;
    [SerializeField] private float maxPickUpDistance;
    [SerializeField] private LayerMask whatIsWeapon;
    [SerializeField] private int weaponsAmount;

    public AWeapon[] Weapons;
    public int currentWeaponID;
    public AWeapon currentWeapon;

    private AWeapon HighLightedWeapon = null;

    private void Start()
    {
        Weapons = new AWeapon[weaponsAmount];
    }

    private void Update()
    { 


        CheckForNewWeapon();

        if (HighLightedWeapon != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUpWeapon();
        }

        if(GI.weaponChange)
        {
            ChangeWeapon(GI.weaponSlot);
        }
    }

    private void ChangeWeapon(int index)
    {
        if(index != currentWeaponID && Weapons[index] != null && (currentWeapon == null || currentWeapon.weaponChangePermission))
        {
            currentWeapon.Hide();
            currentWeapon = Weapons[index];
            currentWeapon.Use();
            currentWeaponID = index;
        }
    }

    private void CheckForNewWeapon()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, camTransform.forward, out hit, maxPickUpDistance, whatIsWeapon))
        {
            if (HighLightedWeapon != hit.transform.GetComponent<AWeapon>())
            {
                if (HighLightedWeapon != null)
                {
                    HighLightedWeapon.HighLight(false);
                }
                HighLightedWeapon = hit.transform.GetComponent<AWeapon>();
                HighLightedWeapon.HighLight(true);
            }
        }
        else
        {
            if (HighLightedWeapon != null)
            {
                HighLightedWeapon.HighLight(false);
            }
            HighLightedWeapon = null;
        }
    }

    void PickUpWeapon()
    {
        if(currentWeapon != null)
        {
            if(!currentWeapon.weaponChangePermission)
            {
                return;
            }
            currentWeapon.Hide();
        }
        int slot = FindSlot();
        if (slot != -1)
        {
            HighLightedWeapon.HighLight(false);
            HighLightedWeapon.Player = gameObject;
            HighLightedWeapon.Use();
            Weapons[slot] = HighLightedWeapon;
            currentWeapon = HighLightedWeapon;
            currentWeaponID = slot;
        }
    }

    int FindSlot() //zwraca pierwszy dostepny slot, jesli nie ma wolnych slotow zwraca -1
    {
        for(int i=0; i<weaponsAmount; i++)
        {
            if(Weapons[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
