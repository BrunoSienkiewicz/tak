using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    InUse,
    Hidden,
    Item
};

public class NewWeapon : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] protected Animator animator;
    public GameObject Player;
    public WeaponState currentState;

    public virtual void Hide()
    {
        PickUp();
        animator.SetTrigger("hide");
        currentState = WeaponState.Hidden;
    }

    public virtual void Throw()
    {
        currentState = WeaponState.Item;
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
        animator.SetTrigger("throw");
    }

    public virtual void Use()
    {
        PickUp();
        animator.SetTrigger("show");
        currentState = WeaponState.InUse;
    }

    public virtual void HighLight(bool on)
    {
        if(on)
        {
            transform.localScale *= 1.1f;
        }
        else
        {
            transform.localScale /= 1.1f;
        }
    }

    protected virtual void PickUp()
    {
        if (currentState == WeaponState.Item)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            transform.position = Vector3.zero;
            transform.rotation = new Quaternion(0,0,0,0);
            transform.SetParent(Player.GetComponent<PlayerWeaponManager>().camTransform, false);
        }
    }
}
