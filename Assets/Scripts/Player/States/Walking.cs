using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : PlayerState
{
    private float moveSpeed = 3f;

    public Walking(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Idling), Not(LeftOrRight)));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling)));
        transitionsTo.Add(new Transition(typeof(Attacking), LeftClick));
        transitionsTo.Add(new Transition(typeof(Rolling), LeftOrRight, Ctrl));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Running");
    }

    public override void DuringExecution()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movement.SetVelocity(-transform.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.SetVelocity(transform.right * moveSpeed);
        }
    }
}
