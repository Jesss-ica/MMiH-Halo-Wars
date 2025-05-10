using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bat : Agent
{
    private float CooldownDuration = 3.2f;
    private int HealthPoints = 4;
    private char type = 'b';



    [SerializeField] public GameObject projectile;
    [SerializeField] private Transform projectileSpawnPoint;


    private void Start()
    {
        SetStats(HealthPoints, type);
        AgentStart();
    }
    void Update()
    {
        AgentUpdate();
        if (isPlayerClose())
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (IsAvailable)
        {
            StartCoroutine(StartCooldown(CooldownDuration));

            Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
        }
    }
}
