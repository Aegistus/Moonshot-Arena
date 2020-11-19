using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class JumpPad : MonoBehaviour
{
    public float force = 100f;

    private float soundResetTimer = 2f;
    private float timer = 0;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(-transform.forward * force);
            if (timer == 0)
            {
                AudioManager.instance.StartPlayingAtPosition("Jump Pad Boost", transform.position);
                timer = soundResetTimer;
            }
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, 100);
        }
    }
}
