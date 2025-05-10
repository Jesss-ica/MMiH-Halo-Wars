using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBuller : Projectile
{
    public int speed;
    public int damage;
    public float lifeSpan;

    public GameObject zombie;
    public GameObject bat;
    public GameObject spider;

    public GameObject doorToAssign;
    
    
    void Start()
    {
        SetStats(speed, damage, lifeSpan);
        ProjectileStart();
    }

    void Update()
    {
        ProjectileUpdate();
    }

    public void SetDoorToAssign(GameObject DoorToAssign)
    {
        doorToAssign = DoorToAssign;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        PickRandomSpawn();
    }

    void PickRandomSpawn() //By MegadethRocks (2011), Avaliable at: https://discussions.unity.com/t/random-numbers-and-chance/438777
    {
        GameObject enemyCache;
        float randValue = Random.value;
        if (randValue <= 0.33f)
        {
            enemyCache = Instantiate(zombie, gameObject.transform.position, transform.rotation);
        }
        else if (randValue <= 0.66f)
        {
            enemyCache = Instantiate(bat, gameObject.transform.position, transform.rotation);
        }
        else
        {
            enemyCache = Instantiate(spider, gameObject.transform.position, transform.rotation);
        }
        enemyCache.GetComponent<Agent>().assignedDoor = doorToAssign;
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
