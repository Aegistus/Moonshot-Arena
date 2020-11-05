using System;
using System.Collections;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public static event Action<int> OnCooldownChange;

    public float thrust = 10f;
    public float cooldownTime = 2f;
    [HideInInspector]
    public int numOfCharges = 2;

    private Rigidbody rb;
    private Transform camTransform;
    private Camera cam;
    private Vector3 boostedVelocity;
    private float startingFOV;
    private float targetFOV;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        camTransform = cam.transform;
        startingFOV = cam.fieldOfView;
        targetFOV = startingFOV;
    }

    private void Update()
    {
        if (numOfCharges > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                boostedVelocity = Vector3.zero;
                if (Input.GetKey(KeyCode.W))
                {
                    boostedVelocity = camTransform.forward;
                    targetFOV = startingFOV + 5;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    boostedVelocity = -camTransform.forward;
                    targetFOV = startingFOV - 5;
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
                } else if (Input.GetKey(KeyCode.Space))
                {
                    boostedVelocity = camTransform.up;
                }
                boostedVelocity *= thrust;
                Boost(boostedVelocity);
            }
        }
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, .1f);
    }

    public void Boost(Vector3 boostedVelocity)
    {
        if (boostedVelocity != Vector3.zero)
        {
            print("Boosted velocity: " + boostedVelocity);
            rb.velocity += boostedVelocity;
            numOfCharges--;
            OnCooldownChange?.Invoke(numOfCharges);
            StartCoroutine(CameraReset());
            StartCoroutine(ThrusterCooldown());
        }
    }

    private IEnumerator ThrusterCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        numOfCharges++;
        OnCooldownChange?.Invoke(numOfCharges);
    }

    private IEnumerator CameraReset()
    {
        yield return new WaitForSeconds(1f);
        camTransform.localRotation = Quaternion.Euler(new Vector3(camTransform.localRotation.eulerAngles.x, 0f, 0f));
        targetFOV = startingFOV;
    }
}
