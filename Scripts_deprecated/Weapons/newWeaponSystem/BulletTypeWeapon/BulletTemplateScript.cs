using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTemplateScript : MonoBehaviour
{
    [HideInInspector] public float speed = 10;
    [HideInInspector] public float damage = 1;

    void Start()
    {
        
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            return;
        }
        Destroy(gameObject);
    }
}
