using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float SmoothDampeningTime;
    float smoothDampeningCurrentVelocity;
    public Transform cam;
    public float bodyHeight;
    public float bodyMoveSpeed;
    public Transform bodyRayCastOrigin;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if(direction.magnitude > 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothDampeningCurrentVelocity, SmoothDampeningTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        if (Physics.Raycast(bodyRayCastOrigin.position, Vector3.down, out RaycastHit rayhit))
        {
            transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, new Ray(rayhit.point, Vector3.up).GetPoint(bodyHeight), bodyMoveSpeed).y, transform.position.z);
        }
    }
}
