using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    public float turnSpeed = 1f;

    private void Start()
    {
        transform.parent = null;
    }

    private void Update()
    {
        if (target != null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            AdjustDirection();
        }
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
        PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Explosion, transform.position, Quaternion.identity, 4f);
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        Explode();
    }
}
