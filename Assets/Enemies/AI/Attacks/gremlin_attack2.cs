using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gremlin_attack2 : MonoBehaviour
{
    public float damage;

    void Start()
    {
        Destroy(gameObject, 0.25f);
    }

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, 720 * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            other.GetComponent<player_stat>().hp -= damage;
    }
}
