using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : PlayerState
{
    private float moveSpeed = 5f;

    public Walking(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Run");
        transitionsTo.Add(new Transition(typeof(Idling), Not(MoveKeys)));
        transitionsTo.Add(new Transition(typeof(Sprinting), Shift));
        transitionsTo.Add(new Transition(typeof(Sliding), Ctrl));
        transitionsTo.Add(new Transition(typeof(Jumping), Spacebar, OnGround));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        movement.SetVelocity(Vector3.zero);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        if (movement.Velocity.magnitude < moveSpeed)
        {
            newVelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                newVelocity += transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                newVelocity += -transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newVelocity += -transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                newVelocity += transform.right;
            }
            newVelocity = newVelocity.normalized;
            movement.SetVelocity(newVelocity * moveSpeed);
            //KeepGrounded();
        }
    }

    //RaycastHit rayHit;
    //private void KeepGrounded()
    //{
    //    if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 1.5f, groundLayer))
    //    {
    //        Debug.Log("Found ground");
    //        transform.position = rayHit.point + Vector3.up * 1f;
    //    }
    //}
}
