using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class DroneState : State
{
    protected Drone drone;

    protected DroneState(GameObject gameObject) : base(gameObject)
    {
        drone = gameObject.GetComponent<Drone>();
    }
}
