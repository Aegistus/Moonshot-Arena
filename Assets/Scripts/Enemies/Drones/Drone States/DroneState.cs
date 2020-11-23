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
    protected ConstructionManager construction;
    protected ConstructionLaser constructionLaser;

    protected DroneState(GameObject gameObject) : base(gameObject)
    {
        drone = gameObject.GetComponent<Drone>();
        scanner = gameObject.GetComponentInChildren<FieldOfView>();
        attack = gameObject.GetComponent<DroneAttack>();
        droneModel = gameObject.transform.GetChild(0);
        construction = ConstructionManager.instance;
        constructionLaser = gameObject.GetComponentInChildren<ConstructionLaser>();
    }

    public Func<bool> PlayerIsInLOS => () => scanner.visibleTargets.Count > 0;
    public Func<bool> AtDestination => () => drone.AtDestination;
    public Func<bool> InsideAttackRadius => () => scanner.visibleTargets[0] != null && Vector3.Distance(transform.position, scanner.visibleTargets[0].position) < minAttackRadius;
    public Func<bool> OutsideAttackRadius => () => scanner.visibleTargets[0] != null && Vector3.Distance(transform.position, scanner.visibleTargets[0].position) > maxAttackRadius;
    public Func<bool> ConstructionAvailable => () => construction.blueprints.Count > 0 && constructionLaser != null;
}
