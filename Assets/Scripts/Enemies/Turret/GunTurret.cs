using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : Turret
{
    public int damage = 1;
    public float shotsPerSecond = 1f;
    public float targetKnockback = 1f;
    public float rotationSpeed = 5f;
    public Transform coreTransform;
    public List<Transform> muzzleTips = new List<Transform>();

    [HideInInspector]
    public bool isShooting = false;

    private RaycastHit rayHit;
    private PoolManager poolManager;

    protected override void Start()
    {
        base.Start();
        poolManager = PoolManager.Instance;
    }

    private void LateUpdate()
    {
        if (isShooting)
        {
            coreTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (playerSeen && !isShooting)
        {
            StartShooting();
        }
        else if (!playerSeen && isShooting)
        {
            StopShooting();
        }
    }

    public void StartShooting()
    {
        isShooting = true;
        StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    private IEnumerator Shoot()
    {
        while (isShooting)
        {
            if (Physics.Raycast(muzzleTips[muzzleIndex].position, muzzleTips[muzzleIndex].forward, out rayHit, 1000f))
            {
                MuzzleFX();
                ImpactFX();
                ShotPhysics();
                ShotDamage();
                AudioManager.instance.StartPlayingAtPosition("Gun Shot 03", muzzleTips[muzzleIndex].position);
            }
            yield return new WaitForSeconds(shotsPerSecond);
        }
    }

    private void ShotDamage()
    {
        Health health = rayHit.collider.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Damage(damage);
        }
    }

    void ShotPhysics()
    {
        if (rayHit.rigidbody != null)
        {
            rayHit.rigidbody.velocity += transform.forward * targetKnockback;
        }
    }

    private int muzzleIndex = 0;
    private void MuzzleFX()
    {
        Transform muzzle = muzzleTips[muzzleIndex];
        poolManager.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.MuzzleFlash, muzzle.position, muzzle.rotation, 5f);
        poolManager.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.BulletTrail, muzzle.position, muzzle.rotation, 3f);
        muzzleIndex++;
        if (muzzleIndex >= muzzleTips.Count)
        {
            muzzleIndex = 0;
        }
    }

    private void ImpactFX()
    {
        poolManager.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.BulletImpact, rayHit.point, Quaternion.Euler(-90,0,0), 2f);
    }
}
