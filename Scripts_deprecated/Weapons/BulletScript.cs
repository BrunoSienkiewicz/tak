using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletspeed;
    public float bulletdamage;
    
    void Update()
    {
        //Debug.Log(bulletspeed);
        transform.position += transform.forward * bulletspeed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Enemy")
        {
            EnemyHealth enemyHealthScript = col.transform.GetComponent<EnemyHealth>();
            enemyHealthScript.DeductHealth(bulletdamage);
        }

        Destroy(this.gameObject);
    }
}
