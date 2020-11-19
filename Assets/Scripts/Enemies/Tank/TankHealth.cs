using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : EnemyHealth
{
    public override void Kill()
    {
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.LargeExplosion, transform.position, Quaternion.identity, 3f);
        Destroy(gameObject);
    }
}
