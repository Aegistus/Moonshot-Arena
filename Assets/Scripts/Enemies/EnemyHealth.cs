using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public int scoreValue = 10;

    protected PoolManager pool;

    private void Start()
    {
        pool = PoolManager.Instance;
    }

    public override void Damage(int damage)
    {
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Electricity, transform.position, Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360), .5f);
        base.Damage(damage);
    }

    public override void Kill()
    {
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.MediumExplosion, transform.position, Quaternion.identity, 3f);
        ScoreManager.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
