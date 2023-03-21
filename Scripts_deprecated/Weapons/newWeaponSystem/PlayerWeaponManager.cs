using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] public Transform camTransform;
    [SerializeField] private float maxPickUpDistance;
    [SerializeField] private LayerMask whatIsWeapon;
    [SerializeField] private int weaponsAmount;

    public NewWeapon[] Weapons;
    public int currentWeaponID;
    public NewWeapon currentWeapon;

    private NewWeapon HighLightedWeapon = null;

    private void Start()
    {
        Weapons = new NewWeapon[weaponsAmount];
    }

    private void Update()
    { 


        CheckForNewWeapon();

        if (HighLightedWeapon != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUpWeapon();
        }
    }

    private void CheckForNewWeapon()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, camTransform.forward, out hit, maxPickUpDistance, whatIsWeapon))
        {
            if (HighLightedWeapon != hit.transform.GetComponent<NewWeapon>())
            {
                if (HighLightedWeapon != null)
                {
                    HighLightedWeapon.HighLight(false);
                }
                HighLightedWeapon = hit.transform.GetComponent<NewWeapon>();
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
