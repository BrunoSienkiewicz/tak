using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic_ball : MonoBehaviour
{
    GameObject player;
    float time = 0.8f;
    Vector3 toPlayer;
    Rigidbody rb;
    float playerDistance;
    public GameObject explosion;
    public float damage;
    public float randomrange = 80;

    public float yScale = 0.0002f, allScale = 90, yConst = 2f; ///NIE RUSZAC TYCH USTAWIEN KURWA



    void Death()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (time <= 0 && time > -10)
        {
            time = -10;

            toPlayer = player.transform.position - transform.position;
            playerDistance = toPlayer.magnitude;
            toPlayer = new Vector3(toPlayer.x, toPlayer.y + playerDistance * yScale + yConst, toPlayer.z);

            rb.useGravity = true;
            rb.AddForce(toPlayer * allScale + new Vector3(Random.Range(-randomrange, randomrange), Random.Range(-randomrange, randomrange), Random.Range(-randomrange, randomrange)));
        }
        /*else if(time == -10)
        {

        }*/
        else
        {
            time -= Time.deltaTime;
            transform.position += new Vector3(0, 0.005f, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            print("dmg");
            collision.collider.GetComponent<player_stat>().hp -= damage;
        }
        else if(collision.collider.GetComponent<magic_ball>() != null)
        {
            return;
        }
        Death();
    }
}
