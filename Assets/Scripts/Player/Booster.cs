using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float thrust = 5f;
    public float cooldownTime = 4f;
    [HideInInspector]
    public bool onCooldown = false;

    private Rigidbody rb;
    private Vector3 boostedVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            boostedVelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                boostedVelocity = transform.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                boostedVelocity = -transform.forward;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                boostedVelocity = -transform.right;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                boostedVelocity = transform.right;
            }
            boostedVelocity *= thrust;
            Boost(boostedVelocity);
        }
    }

    public void Boost(Vector3 boostedVelocity)
    {
        if (onCooldown == false)
        {
            print("Boosted");
            rb.velocity = boostedVelocity;
            StartCoroutine(ThrusterCooldown());
        }
    }

    private IEnumerator ThrusterCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

}
