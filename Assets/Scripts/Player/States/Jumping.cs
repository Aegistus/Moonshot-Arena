using UnityEngine;

public class Jumping : PlayerState
{
    private float jumpForce = 4.85f;
    private float airMoveSpeed = 1f;
    Vector3 startingVelocity;

    public Jumping(GameObject gameObject) : base(gameObject)
    {
        animationNames.Add("Jump");
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Idling), OnGround, Not(Rising), Not(Falling)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Jumping");
        //anim.Play(animationNames[0]);
        movement.AddVelocity(Vector2.up * jumpForce);
    }

    public override void DuringExecution()
    {

    }
}
