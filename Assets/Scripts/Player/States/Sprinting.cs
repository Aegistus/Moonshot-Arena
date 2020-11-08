using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : PlayerState
{
    private float moveSpeed = 7f;

    public Sprinting(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Shift)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys), Not(Shift)));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround), Falling));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Sprinting");
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            newVelocity += transform.forward;
        }
        newVelocity = newVelocity.normalized;
        movement.SetVelocity(newVelocity * moveSpeed);
    }
}
