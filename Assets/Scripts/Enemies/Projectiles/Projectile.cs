using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private Transform player;
    private Vector3 lastPos;
    private int speed;
    private int damage;
    private float lifeSpan;
    public void ProjectileStart()
    {
        player = FindObjectOfType<PlayerController>().transform;
        transform.LookAt(player.position);
        StartCoroutine(StartCooldown(lifeSpan));
    }

    public void ProjectileUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void SetStats(int Speed, int Damage, float LifeSpan)
    {
        this.speed = Speed;
        this.damage = Damage;
        this.lifeSpan = LifeSpan;
    }

    public IEnumerator StartCooldown(float CooldownDuration)
    {
        yield return new WaitForSeconds(CooldownDuration - 1);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}
