using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TargetScript : MonoBehaviour
{
    private Vector3 oldPosition;
    private Vector3 currentPosition;
    private Vector3 newPosition;
    private Vector3 rayhit;
    private float lerp = 0;
    private bool rayCollided;

    public Transform rayCastOrigin;
    public UnityEvent onStepFinished;

    public float DistanceFromPosition { get; private set; }


    private void Start()
    {
        currentPosition = transform.position;
        oldPosition = currentPosition;
    }
    private void Update()
    {
        transform.position = currentPosition;
    }

    public void MoveTarget(float stepLength, float stepOffSet, float steppingHeight, float steppingSpeed)
    {
        if (rayCollided)
        {
            if (DistanceFromPosition > stepLength)
            {
                var positionAddition = (rayhit - oldPosition);
                newPosition = rayhit + positionAddition.normalized * stepOffSet;
                lerp = 0;
            }

        }
        if (lerp < 1)
        {
            currentPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            currentPosition.y += Mathf.Sin(lerp * Mathf.PI) * steppingHeight;
            lerp += Time.deltaTime * steppingSpeed;
        }
        else
        {
            oldPosition = newPosition;
            onStepFinished?.Invoke();
        }
    }

    public void CheckTarget(LayerMask layerToHit)
    {
        rayCollided = Physics.Raycast(rayCastOrigin.position, Vector3.down, out RaycastHit hitInfo, 1, layerToHit);
        if(rayCollided)
        {
            rayhit = hitInfo.point;
            DistanceFromPosition = Vector3.Distance(newPosition, rayhit);
        }
    }
    //public void MoveTarget(float stepOffSet)
    //{
    //    //oldPosition = currentPosition;
    //    var positionAddition = (rayhit - oldPosition);
    //    newPosition = rayhit + positionAddition.normalized * stepOffSet;
    //    //currentPosition = newPosition;
    //    lerp = 0;
    //}

    //public void AnimateLeg(float steppingHeight, float steppingSpeed)
    //{
    //    if (lerp < 1)
    //    {
    //        currentPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
    //        currentPosition.y += Mathf.Sin(lerp * Mathf.PI) * steppingHeight;
    //        lerp += Time.deltaTime * steppingSpeed;
    //    }
    //    else
    //    {
    //        oldPosition = newPosition;
    //    }
    //}

    //public void GetStepDistance(LayerMask layerToHit)
    //{
    //    if (Physics.Raycast(rayCastOrigin.position, Vector3.down, out RaycastHit hitInfo, 1, layerToHit))
    //    {
    //        rayhit = hitInfo.point;
    //        StepDistance = Vector3.Distance(newPosition, rayhit);
    //    }
    //    else StepDistance = 0;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(newPosition, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(currentPosition, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rayhit, 0.1f);
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(oldPosition, 0.1f);
    }
}
