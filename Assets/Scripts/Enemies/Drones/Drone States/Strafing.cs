using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strafing : DroneState
{
    private float timer;
    private float maxTimer = 2f;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float swivelSpeed = 1f;

    public Strafing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Chasing), PlayerIsInLOS, OutsideAttackRadius));
        transitionsTo.Add(new Transition(typeof(Searching), Not(PlayerIsInLOS)));
    }

    public override void AfterExecution()
    {
        
    }

    public override void BeforeExecution()
    {
        Debug.Log("Strafing");
        //drone.SetDestination(transform.position);
        timer = maxTimer;
    }

    public override void DuringExecution()
    {
        if (AtDestination())
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                drone.SetDestination(new Vector3(transform.position.x + (Random.value * 6) - 3, transform.position.y, transform.position.z));
                timer = maxTimer;
            }
        }
        attack.ChargeAttack();
        startRotation = droneModel.rotation;
        if (scanner.visibleTargets[0] != null)
        {
            droneModel.LookAt(scanner.visibleTargets[0].position);
        }
        targetRotation = droneModel.rotation;
        droneModel.rotation = startRotation;
        droneModel.rotation = Quaternion.Slerp(startRotation, targetRotation, swivelSpeed * Time.deltaTime);
    }
}
