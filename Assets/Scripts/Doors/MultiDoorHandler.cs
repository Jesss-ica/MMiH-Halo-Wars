 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiDoorHandler : MonoBehaviour
{
    public Door[] doors;

    private int assignedEnemies;

    private void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].multiDoor = true;
        }
    }

    public void AddEnemies()
    {
        assignedEnemies++;
    }

    public void RemoveEnemies()
    {
        assignedEnemies--;
        if(assignedEnemies == 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].LockDoor();
            }

        }
    }

    public void HaultDoors()
    {
        for(int i = 0;i < doors.Length;i++)
        {
            doors[i].awaitinglocked = false;
            if (doors[i].unlockedDoor == false)
            {
                doors[i].SetHaultDoor();
                doors[i].SetFutureHaultDoors();
            }
        }
    }
}
