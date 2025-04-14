using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using System;

public class Door : MonoBehaviour
{
    private int assignedEnemies;

    [HideInInspector]
    public bool multiDoor;
    [HideInInspector]
    public bool awaitinglocked = false;
    [HideInInspector]
    public bool unlockedDoor = false;

    public GameObject Open;
    public GameObject andShutCase;

    [Header("On MultiDoor Case")]
    public GameObject lockedDoor;
    public GameObject haltdDoor;
    public Door[] FutureHaltDoors;



    /*
    public bool closeBehind;
    public GameObject closeBehindDoor;

#if UNITY_EDITOR
    [CustomEditor(typeof(Door))]
    class MyClassEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Door self = (Door)target;
            serializedObject.Update();
            if (self.closeBehind)
                DrawDefaultInspector();
            else
            {
                DrawPropertiesExcluding(serializedObject, "closeBehindDoor");
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    */

    public void AddEnemy()
    {
        assignedEnemies++;
    }

    public void RemoveEnemy()
    {
        assignedEnemies--;
        if(assignedEnemies == 0)
        {
            if(multiDoor == false)
            {
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        Open.SetActive(true);
        andShutCase.SetActive(false);
        if (multiDoor == true)
        {
            haltdDoor.SetActive(false);
            lockedDoor.SetActive(false);
        }
    }

    public void CloseDoor()
    {
        Open.SetActive(false);
        andShutCase.SetActive(true);
        if(multiDoor == true)
        {
            haltdDoor.SetActive(false);
            lockedDoor.SetActive(false);
        }
    }

    public void LockDoor()
    {
        awaitinglocked = true;
        Open.SetActive(false);
        andShutCase.SetActive(false);
        haltdDoor.SetActive(false);
        lockedDoor.SetActive(true);

    }

    public void UnlockDoor()
    {
        unlockedDoor = true;
        OpenDoor();
        gameObject.transform.parent.gameObject.GetComponent<MultiDoorHandler>().HaultDoors();
    }

    public void SetHaultDoor()
    {
        Open.SetActive(false);
        andShutCase.SetActive(false);
        lockedDoor.SetActive(false);
        haltdDoor.SetActive(true);
    }
    
    public void SetFutureHaultDoors()
    {
        for(int i = 0; i < FutureHaltDoors.Length; i++)
        {
            FutureHaltDoors[i].SetHaultDoor();
        }


    }
}
