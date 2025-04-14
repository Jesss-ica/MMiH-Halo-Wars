using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardsPlayer : MonoBehaviour // Adapted from J-Unity on the Unity Forms : https://discussions.unity.com/t/how-do-i-make-an-object-always-face-the-player/5486/2
{
    public Transform targetPosition;
    public int damp;

    void Update()
    {
        if (targetPosition) // we get sure the target is here
        {
            var rotationAngle = Quaternion.LookRotation(targetPosition.position - transform.position); // we get the angle has to be rotated
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * damp); // we rotate the rotationAngle 
        }
    }
}
