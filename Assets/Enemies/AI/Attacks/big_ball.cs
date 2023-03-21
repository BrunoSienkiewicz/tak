using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class big_ball : MonoBehaviour
{
    GameObject player;
    float time = 2.8f, antitime;
    Vector3 toPlayer;
    Rigidbody rb;
    float playerDistance;
    public float damage;
    public GameObject explosion; 
    public Transform parento; //nibyrodzic

    public float yScale = 0.00001f, allScale = 90, yConst = 2; ///NIE RUSZAC TYCH USTAWIEN KURWA



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

            transform.parent = null;
            rb.useGravity = true;
            rb.AddForce(toPlayer * allScale);
        }
        else if(time > 0)
        {
            time -= Time.deltaTime;
            transform.position += new Vector3(0, 0.002f, 0);
            antitime = ((time-2.8f) * -1)/2.8f;
            transform.localScale = Vector3.one * antitime;

            if(parento != null){
                if(parento.GetComponent<fasolka>().state != "attack1") //jesli gremlin z jakiegos powodu przestanie rzucac czar attack1
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }

            transform.position = new Vector3(parento.position.x, transform.position.y, parento.position.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            print("duzy dmg");
            collision.collider.GetComponent<player_stat>().hp -= damage;
        }
        Death();
    }
}
