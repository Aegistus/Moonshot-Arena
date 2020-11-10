using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroneState : State
{
    protected Drone drone;
    protected FieldOfView scanner;
    protected DroneAttack attack;
    protected Transform droneModel;
    protected float maxAttackRadius = 15f;
    protected float minAttackRadius = 10f;

    protected DroneState(GameObject gameObject) : base(gameObject)
    {
        drone = gameObject.GetComponent<Drone>();
        scanner = gameObject.GetComponent<FieldOfView>();
        attack = gameObject.GetComponent<DroneAttack>();
        droneModel = gameObject.transform.GetChild(0);
    }

    public Func<bool> PlayerIsInLOS => () => scanner.visibleTargets.Count > 0;
    public Func<bool> AtDestination => () => drone.AtDestination;
    public Func<bool> InsideAttackRadius => () => Vector3.Distance(transform.position, scanner.visibleTargets[0].position) < minAttackRadius;
    public Func<bool> OutsideAttackRadius => () => Vector3.Distance(transform.position, scanner.visibleTargets[0].position) > maxAttackRadius;
}
