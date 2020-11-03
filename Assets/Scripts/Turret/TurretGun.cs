using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    public float shotsPerSecond = 1f;
    public List<Transform> muzzleTips = new List<Transform>();

    [HideInInspector]
    public bool isShooting = false;
    private RaycastHit rayHit;

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
            MuzzleFX();
            yield return new WaitForSeconds(shotsPerSecond);
        }
    }

    private PoolManager poolManager;
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
}
