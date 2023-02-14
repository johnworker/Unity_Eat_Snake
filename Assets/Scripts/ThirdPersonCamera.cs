using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public float speed = 1;
    public Transform Target;
    public Camera cam;

    void LateUpdate()
    {
        
    }

    public void Move()
    {
        Vector3 direction = (Target.position - cam.transform.position).normalized;

        Quaternion lookrotation = Quaternion.LookRotation(direction);
    }
}
