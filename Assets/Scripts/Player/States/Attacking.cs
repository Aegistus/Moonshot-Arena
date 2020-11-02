using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Attacking : PlayerState
{
    private float transitionTime = .5f;
    private float timer;

    private Func<bool> TimeUp => () => timer >= transitionTime;

    public Attacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), TimeUp));
        animationNames.Add("Attack1");
        animationNames.Add("Attack2");
        soundNames.Add("Sword Swish 01");
        soundNames.Add("Sword Swish 02");
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Attacking");
        //anim.Play(animationNames[(int)(Random.value * animationNames.Count)]);
        AudioManager.instance.StartPlaying(soundNames[(int)(Random.value * soundNames.Count)]);
        movement.ModifyVelocity(.5f);
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
