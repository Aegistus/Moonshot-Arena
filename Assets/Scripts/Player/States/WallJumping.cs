using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumping : PlayerState
{
    Vector3 direction;
    float jumpBoost = 4f;

    public WallJumping(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Sliding), OnGround, Ctrl));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Falling), OnGround));
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        if (Physics.Raycast(new Ray(transform.position, transform.right), 1f, groundLayer))
        {
            direction = -transform.right;
        }
        if (Physics.Raycast(new Ray(transform.position, -transform.right), 1f, groundLayer))
        {
            direction = transform.right;
        }
        movement.AddVelocity((direction + transform.up) * jumpBoost);
    }

    public override void DuringExecution()
    {

    }
}
