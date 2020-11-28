using System;
using System.Collections;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public static event Action<int> OnCooldownChange;

    public ParticleSystem boostParticles;
    public float thrust = 10f;
    public float cooldownTime = 2f;
    [HideInInspector]
    public int numOfCharges = 2;

    private Transform camTransform;
    private PlayerController player;
    private CameraFX camFX;
    private Vector3 boostedVelocity;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        camTransform = Camera.main.transform;
        camFX = Camera.main.GetComponentInParent<CameraFX>();
        boostParticles.Stop();
    }

    private void Update()
    {
        if (numOfCharges > 0)
        {
            if (player.CurrentState.GetType() == typeof(Falling) || player.CurrentState.GetType() == typeof(Jumping) || player.CurrentState.GetType() == typeof(WallJumping))
            {
                boostedVelocity = Vector3.zero;
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        boostedVelocity += camTransform.forward;
                        camFX.AddTargetFOV(10);
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        boostedVelocity += -camTransform.forward;
                        camFX.AddTargetFOV(-10);
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        boostedVelocity += -camTransform.right;
                        camFX.AddTargetRotation(10f);
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        boostedVelocity += camTransform.right;
                        camFX.AddTargetRotation(-10f);
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    boostedVelocity += -camTransform.up;
                    if (Input.GetKey(KeyCode.W))
                    {
                        boostedVelocity += camTransform.forward;
                        camFX.AddTargetFOV(10);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    boostedVelocity += camTransform.up;
                    if (Input.GetKey(KeyCode.W))
                    {
                        boostedVelocity += camTransform.forward;
                        camFX.AddTargetFOV(10);
                    }
                }
                if (boostedVelocity != Vector3.zero)
                {
                    boostedVelocity = boostedVelocity.normalized;
                    boostedVelocity *= thrust;
                    Boost(boostedVelocity);
                    if (boostedVelocity.magnitude > 0)
                    {
                        AudioManager.instance.StartPlaying("Rocket Boost");
                    }
                }
            }
        }
    }

    public void Boost(Vector3 boostedVelocity)
    {
        if (boostedVelocity != Vector3.zero)
        {
            player.AddVelocity(boostedVelocity);
            numOfCharges--;
            OnCooldownChange?.Invoke(numOfCharges);
            boostParticles.Play();
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
        camFX.ResetFOV();
        camFX.ResetRotation();
    }
}
