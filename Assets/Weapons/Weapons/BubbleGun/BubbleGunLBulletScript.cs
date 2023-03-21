using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubbleGunLBulletScript : GenericBullet
{
    public float scaleIncrease = 0.999f;
    public float speedIncrease = 1.001f;
    public float damageIncrease = 1.02f;
    public float scaleCap = 0.3f;
    public float speedCap = 40f;
    public float damageCap = 100f;
    private float timeElapsed = 0f;
    private Vector3 startScale;
    private float startSpeed = 0f;
    private float startDamage = 0f;
    private ParticleSystem bulletParticleSystem;

    protected override void Start()
    {
        bulletParticleSystem = GetComponentInChildren<ParticleSystem>();
        startScale = transform.localScale;
        startSpeed = speed;
        startDamage = damage;
    }
    protected override void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if (transform.localScale.x >= scaleCap)
        {
            transform.localScale = startScale * Mathf.Pow(scaleIncrease, timeElapsed);
        }
        if (speed <= speedCap)
        {
            speed = startSpeed * Mathf.Pow(speedIncrease, timeElapsed);
        }
        if (damage <= damageCap)
        {
            damage = startDamage * Mathf.Pow(damageIncrease, timeElapsed);
        }

        var main = bulletParticleSystem.main;
        main.startSize = transform.localScale.x * 1.1f;
    }

    /*private void OnCollisionEnter(Collision other)
    {
        print("collision");
        if (other.collider.tag == "Bullet")
        {
            return;
        }
        if (other.collider.tag == "Player")
        {
            return;
        }
        if (other.collider.gameObject.layer == 9)
        {
            return;
        }
        if(other.collider.tag == "Enemy")
        {
            print("enemy");
            other.collider.GetComponent<enemy_stat>().hp -= damage;
        }
        print(other.collider.tag);
        Destroy(gameObject);
    }*/
}
