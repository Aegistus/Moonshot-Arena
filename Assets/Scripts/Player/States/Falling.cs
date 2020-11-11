using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : PlayerState
{
    public Falling(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Fall");
        transitionsTo.Add(new Transition(typeof(Sliding), OnGround, Ctrl));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Falling), OnGround));
        transitionsTo.Add(new Transition(typeof(WallJumping), Spacebar, NextToWall));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Falling");
        //anim.Play(animationNames[0]);
    }

    public override void DuringExecution()
    {

    }
}
