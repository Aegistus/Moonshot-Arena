using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TakingDamage : PlayerState
{
    private float maxTime = .5f;
    private float timer;

    Func<bool> TimeUp => () => timer >= maxTime;

    public TakingDamage(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Hurt");
        soundNames.Add("Gore Sound 01");
        soundNames.Add("Gore Sound 02");
        transitionsTo.Add(new Transition(typeof(Idling), TimeUp));
    }

    public override void AfterExecution()
    {
        timer = 0;
    }

    public override void BeforeExecution()
    {
        AudioManager.instance.StartPlaying(soundNames[(int)(Random.value * soundNames.Count)]);
        //anim.Play(animationNames[0]);
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
