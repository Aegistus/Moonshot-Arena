using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strafing : PlayerState
{
    public Strafing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(LeftOrRight)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        
    }

    public override void DuringExecution()
    {
        
    }
}
