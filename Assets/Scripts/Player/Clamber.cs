using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamber : MonoBehaviour
{
    public Transform eyeLevel;
    public Transform hipLevel;
    public float scanDistance = .5f;
    public float resetTime = .5f;

    public LayerMask groundLayer;

    private CapsuleCollider capsuleCollider;
    private SphereCollider sphereCollider;
    private PlayerController movement;
    private CameraFX camFX;

    private void Start()
    {
        capsuleCollider = GetComponentInParent<CapsuleCollider>();
        sphereCollider = GetComponentInParent<SphereCollider>();
        movement = GetComponentInParent<PlayerController>();
        camFX = GetComponentInChildren<CameraFX>();
        sphereCollider.enabled = false;
    }

    private void Update()
    {
        if (Physics.Raycast(new Ray(hipLevel.position, hipLevel.forward), scanDistance, groundLayer))
        {
            if (!Physics.Raycast(new Ray(eyeLevel.position, eyeLevel.forward), scanDistance, groundLayer))
            {
                movement.AddVelocity(transform.forward * 1f);
                capsuleCollider.enabled = false;
                sphereCollider.enabled = true;
                StartCoroutine("ResetColliders");
                print("Clambering");
            }
        }
    }

    private IEnumerator ResetColliders()
    {
        yield return new WaitForSeconds(resetTime);
        capsuleCollider.enabled = true;
        sphereCollider.enabled = false;
        //movement.AddVelocity(-transform.up * 1f);
    }
}
