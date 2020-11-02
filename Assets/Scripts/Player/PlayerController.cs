using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;

    public Vector3 velocity;
    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
            {typeof(Jumping), new Jumping(gameObject) },
            {typeof(Attacking), new Attacking(gameObject) },
            {typeof(Blocking), new Blocking(gameObject) },
            //{typeof(Rolling), new Rolling(gameObject) },
            {typeof(TakingDamage), new TakingDamage(gameObject) },
            {typeof(Falling), new Falling(gameObject) },
            //{typeof(GrabbingLedge), new GrabbingLedge(gameObject) }
        };
        stateMachine.SetStates(states, typeof(Idling));
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void AddVelocity(Vector3 additional)
    {
        velocity += additional;
    }

    public void ModifyVelocity(float percentChange)
    {
        velocity *= percentChange;
    }

    private void Update()
    {
        stateMachine.ExecuteState();
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

}
