using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegController : MonoBehaviour
{
    private bool takeNewStep;
    public List<TargetScript> targets = new List<TargetScript>();
    private TargetScript targetToChange;
    public float stepLength;
    public LayerMask layerToHit;
    public float stepOffSet;
    public float steppingHeight;
    public float steppingSpeed;


    private void Start()
    {
        targetToChange = targets.First();
    }

    private void Update()
    {
        foreach(TargetScript target in targets)
        {
            target.CheckTarget(layerToHit);
        }
        if(takeNewStep)
        {
            targetToChange = targets.OrderByDescending(t => t.DistanceFromPosition).First();
            takeNewStep = false;
        }
        targetToChange.MoveTarget(stepLength, stepOffSet, steppingHeight, steppingSpeed);





        //targets.OrderByDescending(t => t.StepDistance).First();
        //foreach (TargetScript target in targets)
        //{
        //    target.GetStepDistance(layerToHit);
        //    Debug.Log(target.name + " " + target.StepDistance);
        //    if(targetToChange == null || target.StepDistance > targetToChange.StepDistance)
        //    {
        //        targetToChange = target;
        //        target.AnimateLeg(steppingHeight, steppingSpeed);
        //    }
        //}
        //targetToChange.MoveTarget(stepOffSet);
        //targetToChange = null;
    }

    public void TakeNewStep()
    {
        takeNewStep = true;
    }
}
