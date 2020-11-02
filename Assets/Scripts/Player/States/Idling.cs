using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : PlayerState
{    
    public Idling(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Idle");
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Walking), MoveKeys));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling), Not(Rising)));
        transitionsTo.Add(new Transition(typeof(Attacking), LeftClick));
        //transitionsTo.Add(new Transition(typeof(Blocking), RightClick));

        transitionsTo.Add(new Transition(typeof(TakingDamage), () => Input.GetKeyDown(KeyCode.Q)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Idling");
        //anim.Play(animationNames[0]);
        movement.SetVelocity(Vector3.zero);
        if (OnGround())
        {
            rb.velocity = Vector3.zero;
        }
    }

    public override void DuringExecution()
    {

    }
}
