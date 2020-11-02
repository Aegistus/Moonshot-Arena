using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PlayerState
{
    private float jumpForce = 4.85f;
    private float airMoveSpeed = 1f;
    private Vector3 startingVelocity;

    public Jumping(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Jump");
        transitionsTo.Add(new Transition(typeof(Attacking), LeftClick));
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Idling), OnGround, Not(Rising), Not(Falling)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Jumping");
        //anim.Play(animationNames[0]);
        rb.velocity = Vector2.up * jumpForce;
        startingVelocity = movement.velocity;
    }

    public override void DuringExecution()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movement.SetVelocity(-transform.right * airMoveSpeed + startingVelocity);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.SetVelocity(transform.right * airMoveSpeed + startingVelocity);
        }
    }
}
