using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Agent
{
    private int HealthPoints = 3;
    private int damage = 5;
    private char type = 'z';
    private float CooldownDuration = 0.5f;

    private void Start()
    {
        SetStats(HealthPoints, type);
        AgentStart();
    }
    void Update()
    {
        AgentUpdate();
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsAvailable)
        {
            StartCoroutine(StartCooldown(CooldownDuration));
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
