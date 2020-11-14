using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelHealth : Health
{
    private PoolManager pool;

    private void Start()
    {
        pool = PoolManager.Instance;
    }

    public override void Kill()
    {
        print("Destroying barrel");
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.LargeExplosion, transform.position, transform.rotation, 3f);
        Destroy(gameObject);
    }
}
