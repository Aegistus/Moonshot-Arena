using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : PlayerState
{
    private float initialBoost = 5f;
    private Transform camTransform;
    private float slideDuck = .25f;
    private CapsuleCollider standingCollider;

    public Sliding(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Ctrl)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys), Not(Ctrl)));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround), Falling));
        //camTransform = Camera.main.transform;
        standingCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    public override void AfterExecution()
    {
        //transform.localPosition = Vector3.zero;
        standingCollider.direction = 1;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Sliding");
        standingCollider.direction = 2;
        transform.Translate(0, -slideDuck, 0, Space.Self);
        movement.AddVelocity(transform.forward * initialBoost);
        movement.AddVelocity(-transform.up);
        AudioManager.instance.StartPlaying("Slide");
    }

    public override void DuringExecution()
    {
        
    }
}
