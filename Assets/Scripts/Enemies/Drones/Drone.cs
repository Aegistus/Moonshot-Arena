using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
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
    }

    public void SetDestination(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }
}
