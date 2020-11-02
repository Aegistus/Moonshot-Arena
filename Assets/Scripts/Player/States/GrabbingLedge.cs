using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingLedge : PlayerState
{
    public GrabbingLedge(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("GrabLedge");
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar));
    }

    public override void AfterExecution()
    {
        //rb.gravityScale = 1;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Grabbing Ledge");
        movement.velocity = Vector3.zero;
        rb.velocity = Vector2.zero;
        //rb.gravityScale = 0;
        transform.Translate(Vector3.up * .85f);
        //anim.Play(animationNames[0]);
    }

    public override void DuringExecution()
    {
        
    }
}
