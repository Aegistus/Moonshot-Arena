using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShellTrail : PoolObject
{
    public float bulletSpeed = 10f;

    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = PoolManager.Instance;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        poolManager.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.LargeExplosion, transform.position, Quaternion.Euler(-90, 0, 0), 2f);
        gameObject.SetActive(false);
    }
}
