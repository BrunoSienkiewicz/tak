using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_stat : MonoBehaviour
{
    public float hp;
    public GameObject explosion, explosion2;
    Text HPtext;



    void Death()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(explosion2, transform.position, transform.rotation);
        Camera.main.transform.parent = null;
        Destroy(gameObject);
    }



    void Start()
    {
        HPtext = GameObject.Find("HPtext").GetComponent<Text>();
    }

    void FixedUpdate()
    {
        if(hp <= 0)
        {
            Death();
        }

        HPtext.text = "HP: " + hp;
    }
}
