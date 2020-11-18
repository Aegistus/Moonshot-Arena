using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Constructing : DroneState
{
    private BlueprintEnemy targetBlueprint;
    private ConstructionLaser laser;
    private float addProgressInterval = 1f;
    private float timer;

    public Func<bool> NullBlueprint => () => targetBlueprint == null;

    public Constructing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Chasing), PlayerIsInLOS));
        transitionsTo.Add(new Transition(typeof(Patrolling), Not(ConstructionAvailable)));
        transitionsTo.Add(new Transition(typeof(Patrolling), NullBlueprint));
        laser = gameObject.GetComponentInChildren<ConstructionLaser>();
    }

    public override void AfterExecution()
    {
        laser.EmergencyStop();
        drone.NavAgentSetActive(true);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Constructing");
        timer = 0;
        targetBlueprint = ConstructionManager.instance.GetNearestBlueprint(transform.position);
        if (targetBlueprint != null)
        {
            drone.SetDestination(targetBlueprint.transform.position);
        }
    }

    public override void DuringExecution()
    {
        if (Vector3.Distance(transform.position, targetBlueprint.transform.position) < 10)
        {
            if (timer == 0)
            {
                drone.NavAgentSetActive(false);
            }
            if (timer < addProgressInterval)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if (targetBlueprint != null)
                {
                    targetBlueprint.AddWork(10);
                }
                timer = 0;
                laser.StartConstruct();
            }
        }
        droneModel.LookAt(targetBlueprint.transform.position);
    }
}
