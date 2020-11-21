using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : PlayerState
{
    private float moveSpeed = 10f;
    private CameraFX camFX;

    public Sprinting(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Shift)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys), Not(Shift)));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, OnGround));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        camFX = gameObject.GetComponentInChildren<CameraFX>();
    }

    public override void AfterExecution()
    {
        camFX.BounceHead(false);
        camFX.ResetFOV();
    }

    public override void BeforeExecution()
    {
        Debug.Log("Sprinting");
        camFX.BounceHead(true);
        camFX.AddTargetFOV(10f);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            newVelocity += transform.forward;
        }
        newVelocity = newVelocity.normalized;
        movement.SetVelocity(newVelocity * moveSpeed);
        //KeepGrounded();
    }

    //RaycastHit rayHit;
    //private void KeepGrounded()
    //{
    //    if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 1.5f, groundLayer))
    //    {
    //        Debug.Log("Found ground");
    //        transform.position = rayHit.point + Vector3.up;
    //    }
    //}

}