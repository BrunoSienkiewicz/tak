using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalaktyt : MonoBehaviour
{
    GameObject player;
    Vector3 toPlayer;
    Rigidbody rb;
    float playerDistance;
    public float damage;

    public float yScale = 0.004f, allScale = 25, yConst = 20;



    void Death()
    {
        //Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        toPlayer = player.transform.position - transform.position;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 2, 0);

        playerDistance = toPlayer.magnitude;
        toPlayer = new Vector3(toPlayer.x, toPlayer.y + playerDistance * yScale + yConst, toPlayer.z);
        rb.AddForce(toPlayer * allScale);
    }

    void FixedUpdate()
    {
        Quaternion rot = Quaternion.LookRotation(rb.velocity, Vector3.up);
        rot *= Quaternion.AngleAxis(90, Vector3.right); //waznee
        transform.rotation = rot;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            print("stalaktyt dmg");
            collision.collider.GetComponent<player_stat>().hp -= damage;
        }
        Death();
    }
}
