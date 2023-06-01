using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    public float bodyHeight;
    public float bodyMoveSpeed;
    public Transform bodyRayCastOrigin;
    void Update()
    {
        if(Physics.Raycast(bodyRayCastOrigin.position, Vector3.down, out RaycastHit rayhit))
        {
            transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, new Ray(rayhit.point, Vector3.up).GetPoint(bodyHeight), bodyMoveSpeed).y, transform.position.z);
        }
    }
}
