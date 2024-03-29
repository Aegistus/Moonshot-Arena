﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chasing : DroneState
{
    Transform target;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    float swivelSpeed = 1f;

    public Func<bool> TargetIsNull => () => target == null;

    public Chasing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Patrolling), Not(PlayerIsInLOS)));
        transitionsTo.Add(new Transition(typeof(Patrolling), TargetIsNull));
        transitionsTo.Add(new Transition(typeof(Strafing), PlayerIsInLOS, InsideAttackRadius));
    }

    public override void AfterExecution()
    {
        drone.SetDestination(transform.position);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Chasing");
        target = scanner.visibleTargets[0];
    }

    public override void DuringExecution()
    {
        if (target != null)
        {
            drone.SetDestination(target.position);

            attack.ChargeAttack();

            startRotation = droneModel.rotation;
            droneModel.LookAt(target.position);
            targetRotation = droneModel.rotation;
            droneModel.rotation = startRotation;
            droneModel.rotation = Quaternion.Slerp(startRotation, targetRotation, swivelSpeed * Time.deltaTime);
        }
    }
}
