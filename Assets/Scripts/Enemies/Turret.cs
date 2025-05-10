using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Turret : Agent
{
    private float CooldownDuration = 6.5f;
    private int HealthPoints = 12;
    private char type = 't';
    private int multiShotAmount = 2;
    private float timeBetweenShots = 0.2f;

    [SerializeField] public GameObject projectile;
    [SerializeField] public GameObject spawnProjectile;
    private Transform projectileSpawnPoint;

    public GameObject temp;

    private void Start()
    {
        AgentStart();
        projectileSpawnPoint = gameObject.transform;
        SetStats(HealthPoints, type);
        Temp(temp);
    }

    void Update()
    {
        Attack();
    }
    public void Attack()
    {
        if (IsAvailable)
        {
            StartCoroutine(StartCooldown(CooldownDuration));
            if(isPlayerClose())
            {
                StartCoroutine(Cum());
            }
        }
    }

    private IEnumerator Cum()
    {
        for (int i = 0; i < multiShotAmount; i++)
        {
            Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            yield return new WaitForSeconds(timeBetweenShots);
        }
        GameObject spawnBulletCache = Instantiate(spawnProjectile, projectileSpawnPoint.position, transform.rotation);
        spawnBulletCache.GetComponent<SpawnerBuller>().SetDoorToAssign(assignedDoor);
    }
}
