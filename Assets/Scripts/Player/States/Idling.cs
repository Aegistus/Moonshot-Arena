using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : PlayerState
{

    public Idling(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Idle");
        transitionsTo.Add(new Transition(typeof(Walking), MoveKeys));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling), Not(Rising)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));

        //transitionsTo.Add(new Transition(typeof(TakingDamage), () => Input.GetKeyDown(KeyCode.Q)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        //anim.Play(animationNames[0]);
        if (movement.Velocity.magnitude < 10)
        {
            movement.SetVelocity(Vector3.zero);
        }
        //if (OnGround())
        //{
        //    rb.velocity = Vector3.zero;
        //}
    }

    public override void DuringExecution()
    {

    }

    RaycastHit rayHit;
    private void KeepGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 1.5f, groundLayer))
        {
            transform.position = rayHit.point + Vector3.up;
        }
    }
}
