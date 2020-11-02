using System.Collections;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float thrust = 5f;
    public float cooldownTime = 4f;
    [HideInInspector]
    public bool onCooldown = false;

    private Rigidbody rb;
    private Transform camTransform;
    private Vector3 boostedVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            boostedVelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                boostedVelocity = camTransform.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                boostedVelocity = -camTransform.forward;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                boostedVelocity = -camTransform.right;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                boostedVelocity = camTransform.right;
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
