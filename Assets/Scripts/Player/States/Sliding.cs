using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : PlayerState
{
    private float initialBoost = 10f;
    //private Transform camTransform;
    private float slideDuck = .25f;
    private CapsuleCollider standingCollider;
    private ParticleSystem slideParticles;

    public Sliding(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Ctrl)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys), Not(Ctrl)));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, Not(Falling)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround), Falling));
        //camTransform = Camera.main.transform;
        standingCollider = gameObject.GetComponent<CapsuleCollider>();
        slideParticles = gameObject.GetComponent<PlayerController>().slideParticles;
        slideParticles.Stop();
    }

    public override void AfterExecution()
    {
        //transform.localPosition = Vector3.zero;
        standingCollider.direction = 1;
        transform.Translate(0, slideDuck, 0, Space.Self);
        slideParticles.Stop();
    }

    public override void BeforeExecution()
    {
        Debug.Log("Sliding");
        standingCollider.direction = 2;
        transform.Translate(0, -slideDuck, 0, Space.Self);
        movement.AddVelocity(transform.forward * initialBoost);
        movement.AddVelocity(-transform.up);
        AudioManager.instance.StartPlaying("Slide");
        slideParticles.Play();
    }

    public override void DuringExecution()
    {
        
    }
}
