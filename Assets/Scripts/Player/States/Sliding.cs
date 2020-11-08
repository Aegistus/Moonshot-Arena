using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : PlayerState
{
    private float moveSpeed = 7f;
    private Transform camTransform;
    private float slideDuck = .5f;

    public Sliding(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Ctrl)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys), Not(Ctrl)));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround), Falling));
        camTransform = Camera.main.transform;
    }

    public override void AfterExecution()
    {
        camTransform.localPosition = Vector3.zero;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Sliding");
        camTransform.Translate(0, -slideDuck, 0, Space.Self);
    }

    public override void DuringExecution()
    {
        
    }
}
