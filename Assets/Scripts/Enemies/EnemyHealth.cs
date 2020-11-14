using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private PoolManager pool;
    private void Start()
    {
        pool = PoolManager.Instance;
    }

    public override void Kill()
    {
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Explosion, transform.position, Quaternion.identity, 3f);
        gameObject.SetActive(false);
    }
}
