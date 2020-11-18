using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : DroneState
{
    private Vector3 currentPatrolPoint;
    private float timer;
    private float maxTimer = 3f;

    public Patrolling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Chasing), PlayerIsInLOS));
        transitionsTo.Add(new Transition(typeof(Constructing), ConstructionAvailable));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Patrolling");
        timer = maxTimer;
        GetNewPatrolPoint();
        drone.SetDestination(currentPatrolPoint);
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
                GetNewPatrolPoint();
                drone.SetDestination(currentPatrolPoint);
                timer = maxTimer;
            }
        }
    }

    private void GetNewPatrolPoint()
    {
        currentPatrolPoint = new Vector3((Random.value * 10) - 5, (Random.value * 10) - 5, (Random.value * 10) - 5);
    }
}
