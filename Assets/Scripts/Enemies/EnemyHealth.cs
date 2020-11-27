using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public int scoreValue = 10;
    public Transform model;

    protected PoolManager pool;

    private void Start()
    {
        pool = PoolManager.Instance;
        if (model == null)
        {
            model = transform;
        }
    }

    public override void Damage(int damage)
    {
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Electricity, model.position, Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360), .5f);
        base.Damage(damage);
    }

    public override void Kill()
    {
        pool.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.MediumExplosion, model.position, Quaternion.identity, 3f);
        ScoreManager.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
