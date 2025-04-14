using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Agent
{
    private float CooldownDuration = 5f;
    private int HealthPoints = 5;
    private char type = 's';

    [SerializeField] public GameObject spiderCloud;
    [SerializeField] private Transform projectileSpawnPoint;

    private void Start()
    {
        SetStats(HealthPoints, type);
        AgentStart();

    }
    void Update()
    {
        AgentUpdate();
        if (inRange(2))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (IsAvailable)
        {
            StartCoroutine(Pause());
            StartCoroutine(StartCooldown(CooldownDuration));

            Instantiate(spiderCloud, projectileSpawnPoint.position, transform.rotation);
        }
    }
}
