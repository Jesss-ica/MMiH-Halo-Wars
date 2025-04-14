using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherScriptThisTimeForTheDoorAgain : MonoBehaviour
{
    public GameObject Door;

    public bool BoxCollider;
    public bool Enviromental;
    public bool boogie;

    private void Start()
    {
        if(Enviromental == true && boogie == false)
        {
            Door.GetComponent<Door>().AddEnemy();
        }
    }

    public void GetHit()
    {
        if (Enviromental == true)
        {
            if(boogie == true)
            {
                Door.SetActive(true);
                Destroy(gameObject);
            }
            else
            {
                Door.GetComponent<Door>().RemoveEnemy();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BoxCollider == true)
        {
            Door.GetComponent<Door>().OpenDoor();
        }
    }
}
