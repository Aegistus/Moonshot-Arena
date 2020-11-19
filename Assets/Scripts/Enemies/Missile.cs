using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    public float turnSpeed = 1f;

    private ParticleSystem smoke;

    private void Awake()
    {
        smoke = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
        }
        else
        {
            Explode();
        }
    }

    private void FixedUpdate()
    {
        AdjustDirection();
    }

    Quaternion startRotation;
    Quaternion targetRotation;
    private void AdjustDirection()
    {
        startRotation = transform.rotation;
        transform.LookAt(target.position);
        targetRotation = transform.rotation;
        transform.rotation = startRotation;
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public void Explode()
    {
        PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.MediumExplosion, transform.position, Quaternion.identity, 4f);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Explode();
    }
}
