using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    InUse,
    Hidden,
    Item
};

public class AWeapon : MonoBehaviour
{
    [System.NonSerialized] public Animator animator;
    [System.NonSerialized] public GameObject Player;
    [System.NonSerialized] public WeaponState currentState;
    [System.NonSerialized] public Stats statsL;
    [System.NonSerialized] public Stats statsR;
    [System.NonSerialized] public bool weaponChangePermission = true;
    [System.NonSerialized] public bool weaponUsePermission = false;
    [System.NonSerialized] public float pickupOffest = 0.2f; // po zmianie broni, nie mozna strzelac przez dana ilosc czasu
    [System.NonSerialized] private float pickupTime;

    [System.NonSerialized] public List<WeaponAction> actionsL = new List<WeaponAction>();
    [System.NonSerialized] public List<WeaponAction> actionsR = new List<WeaponAction>();
    [System.NonSerialized] public Constraints constraintsL = new StandardConstraints();
    [System.NonSerialized] public Constraints constraintsR = new StandardConstraints();
    [System.NonSerialized] public Ammo ammoL;
    [System.NonSerialized] public Ammo ammoR;
    [System.NonSerialized] public Downtime downtime = new Downtime();

    [System.NonSerialized] protected Transform positionOffsetTransform;
    [System.NonSerialized] protected Vector3 positionOffset;

    protected virtual void Update()
    {
        if(Time.time >= pickupTime)
        {
            weaponUsePermission = true;
        }
        if (currentState == WeaponState.InUse && weaponUsePermission)
        {
            ShootingInput();
        }
    }

    public virtual void Hide()
    {
        PickUp();
        animator.SetTrigger("hide");
        currentState = WeaponState.Hidden;

        weaponUsePermission = false;
    }

    public virtual void Throw()
    {
        currentState = WeaponState.Item;
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
        animator.SetTrigger("throw");

        transform.position += positionOffset;
        positionOffsetTransform.position -= positionOffset;

        weaponUsePermission = false;
    }

    public virtual void Use()
    {
        PickUp();
        animator.SetTrigger("show");
        currentState = WeaponState.InUse;

        weaponUsePermission = false;
        pickupTime = Time.time + pickupOffest;
    }

    public virtual void HighLight(bool on)
    {
        animator.SetBool("highlight", on);
    }

    protected virtual void PickUp()
    {
        if (currentState == WeaponState.Item)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;

            transform.position = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);

            positionOffsetTransform.position = positionOffset;

            transform.SetParent(Player.GetComponent<PlayerWeaponManager>().camTransform, false);
        }
    }

    public virtual void AddUpgrade(Upgrade upgrade)
    {
        statsL.AddUpgrade(upgrade);
        statsR.AddUpgrade(upgrade);
    }

    public virtual void RemoveUpgrade(Upgrade upgrade)
    {
        statsL.RemoveUpgrade(upgrade);
        statsR.RemoveUpgrade(upgrade);
    }

    public virtual void Create(WeaponData weaponData) //tworzenie broni z "weaponData"
    {
        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = weaponData.animatorController;

        statsL = new Stats(weaponData.statsL);
        statsR = new Stats(weaponData.statsR);


        pickupOffest = weaponData.pickupOffest;

        if (weaponData.material != null)
        {
            MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
            meshRenderer.material = weaponData.material;
        }
        if (weaponData.model != null)
        {
            GetComponentInChildren<MeshFilter>().mesh = weaponData.model;
        }

        Setup();
    }

    protected virtual void Setup() //podstawowy setup, czyli ustawienie wszystkim domyslnych statystych itp.
    {
        positionOffsetTransform = transform.GetChild(0);
        positionOffset = positionOffsetTransform.localPosition;
        positionOffsetTransform.localPosition = Vector3.zero;

        foreach (WeaponAction actionL in actionsL)
        {
            actionL.stats = statsL;
        }
        foreach (WeaponAction actionR in actionsR)
        {
            actionR.stats = statsR;
        }

        constraintsL.stats = statsL;
        if (ammoL != null)
        {
            constraintsL.ammo = ammoL;
        }

        constraintsR.stats = statsR;
        if (ammoR != null)
        {
            constraintsR.ammo = ammoR;
        }

        if (ammoL != null)
        {
            ammoL.stats = statsL;
        }
        if (ammoR != null)
        {
            ammoR.stats = statsR;
        }

        constraintsL.downtime = downtime;
        constraintsR.downtime = downtime;

        statsL.UpdateUpgrades();
        statsR.UpdateUpgrades();

        currentState = WeaponState.Item;
    }

    protected virtual void ShootingInput()
    {

    }

    protected virtual void ShootL()
    {
        if (constraintsL.ReadyToUse())
        {
            animator.SetTrigger("shootL");
            foreach(WeaponAction actionL in actionsL)
            {
                actionL.Use();
            }
            constraintsL.Use();
        }
    }
    protected virtual void ShootR()
    {
        if (constraintsR.ReadyToUse())
        {
            animator.SetTrigger("shootR");
            foreach (WeaponAction actionR in actionsR)
            {
                actionR.Use();
            }
            constraintsR.Use();
        }
    }
}
