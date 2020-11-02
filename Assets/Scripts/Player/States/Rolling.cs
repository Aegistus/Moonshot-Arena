using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : PlayerState
{
    private float rollTime = .7f;
    private float rollSpeed = 4f;
    private float timer;
    private Vector3 direction;

    public Func<bool> TimeUp => () => timer >= rollTime;

    public Rolling(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Roll");
        transitionsTo.Add(new Transition(typeof(Idling), TimeUp));
        transitionsTo.Add(new Transition(typeof(Attacking), LeftClick));
    }

    public override void AfterExecution()
    {
        timer = 0;
        direction = Vector3.zero;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Rolling");
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector3.left;
        }
        //anim.Play(animationNames[0]);
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
        transform.Translate(direction * rollSpeed * Time.deltaTime);
    }
}
