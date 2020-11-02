using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : PlayerState
{
    public Blocking(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Block");
        transitionsTo.Add(new Transition(typeof(Idling), Not(RightClick)));
    }

    public override void AfterExecution()
    {
        
    }

    public override void BeforeExecution()
    {
        Debug.Log("Blocking");
        //anim.Play(animationNames[0]);
    }

    public override void DuringExecution()
    {
        
    }
}
