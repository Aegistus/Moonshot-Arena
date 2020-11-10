using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : DroneState
{
    Vector3 target;

    public Searching(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Chasing), PlayerIsInLOS));
        transitionsTo.Add(new Transition(typeof(Patrolling), AtDestination));
    }

    public override void AfterExecution()
    {
        
    }

    public override void BeforeExecution()
    {
        target = scanner.LastSeenLocation;
        drone.SetDestination(target);
    }

    public override void DuringExecution()
    {

    }
}
