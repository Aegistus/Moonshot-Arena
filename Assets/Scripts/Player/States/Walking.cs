using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : PlayerState
{
    private float moveSpeed = 5f;

    public Walking(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys)));
        transitionsTo.Add(new Transition(typeof(Sliding), Ctrl));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround), Falling));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Walking");
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        if (movement.velocity.magnitude < moveSpeed)
        {
            newVelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                newVelocity += transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                newVelocity += -transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newVelocity += -transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                newVelocity += transform.right;
            }
            newVelocity = newVelocity.normalized;
            movement.SetVelocity(newVelocity * moveSpeed);
        }
    }
}
