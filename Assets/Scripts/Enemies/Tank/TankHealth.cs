using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : EnemyHealth
{
    public override void Kill()
    {
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.LargeExplosion, transform.position, Quaternion.identity, 3f);
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.TankExplosion, transform.position, Quaternion.identity, 6f);
        Destroy(gameObject);
    }
}
