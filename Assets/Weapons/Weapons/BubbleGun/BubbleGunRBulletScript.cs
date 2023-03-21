using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubbleGunRBulletScript : GenericBullet
{
    public float scaleIncrease = 1.01f;
    public float speedIncrease = -0.5f;
    public float scaleCap = 0.3f;
    public float speedCap = 20f;
    private float timeElapsed = 0f;
    private Vector3 startScale;
    private ParticleSystem bulletParticleSystem;

    protected override void Start()
    {
        bulletParticleSystem = GetComponentInChildren<ParticleSystem>();
        startScale = transform.localScale;
    }
    protected override void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if (transform.localScale.x <= scaleCap)
        {
            transform.localScale = startScale * Mathf.Pow(scaleIncrease, timeElapsed);
        }
        if (speed + speedIncrease * Time.deltaTime > speedCap)
        {
            speed += speedIncrease * Time.deltaTime;
        }
        if (transform.localScale.x > scaleCap)
            Destroy(gameObject);

        var main = bulletParticleSystem.main;
        main.startSize = transform.localScale.x * 1.1f;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            return;
        }
        Destroy(gameObject);
    }*/
}
