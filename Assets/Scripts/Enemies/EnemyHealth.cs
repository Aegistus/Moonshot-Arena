using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected PoolManager pool;

    private void Start()
    {
        pool = PoolManager.Instance;
    }

    public override void Kill()
    {
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.MediumExplosion, transform.position, Quaternion.identity, 3f);
        Destroy(gameObject);
    }
}
