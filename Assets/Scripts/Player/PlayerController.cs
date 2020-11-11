using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;

    public PlayerState CurrentState => (PlayerState)stateMachine.CurrentState;
    public Vector3 Velocity => rb.velocity;

    private float velocityMod = 1f;
    private StateMachine stateMachine;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stateMachine = new StateMachine();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
            {typeof(Jumping), new Jumping(gameObject) },
            {typeof(TakingDamage), new TakingDamage(gameObject) },
            {typeof(Falling), new Falling(gameObject) },
            {typeof(Sliding), new Sliding(gameObject) },
            {typeof(WallJumping), new WallJumping(gameObject) },
        };
        stateMachine.SetStates(states, typeof(Idling));
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity * velocityMod;
    }

    public void AddVelocity(Vector3 additional)
    {
        rb.velocity += additional * velocityMod;
    }

    public void ModifyVelocity(float percentChange)
    {
        velocityMod = percentChange;
        rb.velocity *= velocityMod;
    }

    private void Update()
    {
        stateMachine.ExecuteState();
        //transform.Translate(velocity * Time.deltaTime, Space.World);
    }

}
