using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public int damage = 100;

    private void Update()
    {
        transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponentInParent<Health>();
        if (health != null)
        {
            health.Damage(damage);
        }
        Explode();
    }

    public void Explode()
    {
        PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.MediumExplosion, transform.position, Quaternion.identity, 4f);
        Destroy(gameObject);
    }
}
