using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBullet : Projectile
{
    public int speed;
    public int damage;
    public float lifeSpan;


    void Start()
    {
        SetStats(speed, damage, lifeSpan);
        ProjectileStart();
    }

    void Update()
    {
        ProjectileUpdate();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
