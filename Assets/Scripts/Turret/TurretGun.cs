using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    public float shotsPerSecond = 1f;
    public float targetKnockback = 1f;
    public List<Transform> muzzleTips = new List<Transform>();

    [HideInInspector]
    public bool isShooting = false;

    private RaycastHit rayHit;
    private PoolManager poolManager;

    private void Start()
    {
        poolManager = PoolManager.Instance;
        StartShooting();
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
            }
            yield return new WaitForSeconds(shotsPerSecond);
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
        print("Shooting");
        Transform muzzle = muzzleTips[muzzleIndex];
        poolManager.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.MuzzleFlash, muzzle.position, muzzle.rotation, 5f);
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
