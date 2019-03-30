using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> targets;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    Vector3 FindCenterPoint() { 
     if (targets.Count == 0)
         return Vector3.zero;
     if (targets.Count == 1)
         return targets[0].position;
     var bounds = new Bounds(targets[0].position, Vector3.zero);
     for (var i = 1; i<targets.Count; i++)
         bounds.Encapsulate(targets[i].position); 
     return bounds.center;
 }

void FixedUpdate()
    {
        Vector3 desiredPosition = FindCenterPoint() + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
