using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        Explode();
    }

    public void Explode()
    {
        PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.MediumExplosion, transform.position, Quaternion.identity, 4f);
        Destroy(gameObject);
    }
}
