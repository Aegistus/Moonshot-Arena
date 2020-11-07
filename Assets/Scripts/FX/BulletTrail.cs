using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : PoolObject
{
    public float bulletSpeed = 10f;

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}
