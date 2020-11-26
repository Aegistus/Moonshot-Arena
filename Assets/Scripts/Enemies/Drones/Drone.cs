using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    public LayerMask ground;
    public Transform droneModel;

    public bool AtDestination { get; private set; }

    private StateMachine stateMachine;
    private NavMeshAgent navAgent;

    private void Start()
    {
        stateMachine = new StateMachine();
        navAgent = GetComponent<NavMeshAgent>();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            { typeof(Patrolling), new Patrolling(gameObject) },
            { typeof(Chasing), new Chasing(gameObject) },
            { typeof(Searching), new Searching(gameObject) },
            { typeof(Strafing), new Strafing(gameObject) },
            { typeof(Constructing), new Constructing(gameObject) },
        };
        stateMachine.SetStates(states, typeof(Patrolling));
    }

    private void Update()
    {
        stateMachine.ExecuteState();
        if (Vector3.Distance(transform.position, navAgent.destination) < 1)
        {
            AtDestination = true;
        }
        else
        {
            AtDestination = false;
        }
        DuckUnderObstacles();
    }

    public void SetDestination(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }

    public void NavAgentSetActive(bool active)
    {
        navAgent.enabled = active;
    }

    private void DuckUnderObstacles()
    {
        if (Physics.Raycast(transform.position, transform.up, ground, 1))
        {
            droneModel.Translate(transform.up * Time.deltaTime);
        }
    }
}
