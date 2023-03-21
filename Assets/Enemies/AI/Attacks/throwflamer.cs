using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwflamer : MonoBehaviour
{
    public float damage = 0.333f;

    void OnTriggerStay(Collider other)
    {
        print("aaaa");
        if(other.tag == "Player")
        {
            print("b");
            other.GetComponent<player_stat>().hp -= damage;
        }
    }
}
