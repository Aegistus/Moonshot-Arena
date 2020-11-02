using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State
{
    protected LayerMask groundLayer;
    protected PlayerController movement;
    protected Rigidbody rb;
    protected List<string> animationNames = new List<string>();
    protected List<string> soundNames = new List<string>();
    //protected Animator anim;

    public Func<bool> MoveKeys => () => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
    public Func<bool> Spacebar => () => Input.GetKeyDown(KeyCode.Space);
    public Func<bool> LeftClick => () => Input.GetMouseButtonDown(0);
    public Func<bool> RightClick => () => Input.GetMouseButton(1);
    public Func<bool> Shift => () => Input.GetKey(KeyCode.LeftShift);
    public Func<bool> Ctrl => () => Input.GetKey(KeyCode.LeftControl);
    public Func<bool> OnGround => () => IsGrounded();
    public Func<bool> Rising => () => rb.velocity.y > 0;
    public Func<bool> Falling => () => rb.velocity.y < 0;

    public PlayerState(GameObject gameObject) : base(gameObject)
    {
        movement = gameObject.GetComponent<PlayerController>();
        //anim = gameObject.GetComponentInChildren<Animator>();
        groundLayer = gameObject.GetComponent<PlayerController>().groundLayer;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private bool IsGrounded()
    {
        if (Physics.BoxCast(transform.position, Vector3.one / 20, Vector3.down, transform.rotation, 1f, groundLayer))
        {
            return true;
        }
        return false;
    }
}
