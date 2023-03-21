using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage_impulse : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<player_stat>().hp -= damage;
        }
        Destroy(gameObject);
    }
}
