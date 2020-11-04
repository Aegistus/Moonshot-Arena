﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;

    public Vector3 velocity;

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
            {typeof(Attacking), new Attacking(gameObject) },
            {typeof(TakingDamage), new TakingDamage(gameObject) },
            {typeof(Falling), new Falling(gameObject) }
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
        velocityMod = percentChange;
    }

    private void Update()
    {
        stateMachine.ExecuteState();
        transform.Translate(velocity * velocityMod * Time.deltaTime, Space.World);
        rb.velocity *= velocityMod;
    }

}
