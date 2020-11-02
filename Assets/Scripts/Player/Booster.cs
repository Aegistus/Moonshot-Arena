using System.Collections;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float thrust = 10f;
    public float cooldownTime = 3f;
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
        if (onCooldown == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
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
                    camTransform.Rotate(new Vector3(camTransform.rotation.x, 0, 10f));
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    boostedVelocity = camTransform.right;
                    camTransform.Rotate(new Vector3(camTransform.rotation.x, 0, -10f));
                }
                boostedVelocity *= thrust;
                Boost(boostedVelocity);
            }
        }
    }

    public void Boost(Vector3 boostedVelocity)
    {
        print("Boosted velocity: " + boostedVelocity);
        rb.velocity = boostedVelocity;
        StartCoroutine(CameraReset());
        StartCoroutine(ThrusterCooldown());
    }

    private IEnumerator ThrusterCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

    private IEnumerator CameraReset()
    {
        yield return new WaitForSeconds(1f);
        camTransform.localRotation = Quaternion.Euler(new Vector3(camTransform.localRotation.eulerAngles.x, 0f, 0f));
    }
}
