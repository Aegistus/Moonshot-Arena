using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : DroneState
{
    private Transform currentPatrolPoint;
    private float patrolRadius = 30f;
    private float timer;
    private float maxTimer = 3f;

    public Patrolling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Chasing), PlayerIsInLOS));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Patrolling");
        timer = maxTimer;
        currentPatrolPoint = PatrolPointManager.current.GetRandomPointCloseToArea(transform.position, patrolRadius);
        drone.SetDestination(currentPatrolPoint.position);
    }

    public override void DuringExecution()
    {
        if (drone.AtDestination)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            } 
            else
            {
                currentPatrolPoint = PatrolPointManager.current.GetRandomPointCloseToArea(transform.position, patrolRadius);
                drone.SetDestination(currentPatrolPoint.position);
                timer = maxTimer;
            }
        }
    }
}
