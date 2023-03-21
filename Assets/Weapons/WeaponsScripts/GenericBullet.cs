using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBullet : MonoBehaviour
{
    [HideInInspector] public float speed = 10;
    [HideInInspector] public float damage = 1;
    [HideInInspector] public int bounced = 0;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector3 lastVelocity;
    //TODO: odbijanie itp.

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        gameObject.layer = 10; // Bullet Layer
    }

    protected virtual void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            return;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("dziala");
        if (collision.gameObject.CompareTag("Map") /*&& bounced>MaxBounce*/)
        {
            BounceOffWall(collision, lastVelocity, rb);
        }
    }

    protected virtual void BounceOffWall(Collision coll, Vector3 lastVelocity, Rigidbody rb)
    {
        var BounceSpeed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(BounceSpeed, 0f);
        bounced++;
    }
}
