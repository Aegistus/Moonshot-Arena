using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GT2;

public class TankTurret : Turret
{
    public int damage = 50;
    public float shotInterval = 6f;
    public float targetKnockback = 20f;
    public float rotationSpeed = 5f;
    public Transform coreTransform;
    public List<Transform> muzzleTips = new List<Transform>();

    [HideInInspector]
    public bool isShooting = false;

    private RaycastHit rayHit;
    private PoolManager poolManager;
    private FieldOfView fov;
    private TurretAim aim;

    private Coroutine shootingCoroutine;

    protected override void Start()
    {
        base.Start();
        poolManager = PoolManager.Instance;
        fov = GetComponentInParent<FieldOfView>();
        aim = GetComponent<TurretAim>();
    }

    private void LateUpdate()
    {
        if (isShooting && fov.visibleTargets.Count > 0 && fov.visibleTargets[0] != null)
        {
            aim.AimPosition = fov.visibleTargets[0].position;
        }
        if (fov.visibleTargets.Count > 0 && !isShooting)
        {
            StartShooting();
        }
        else if (fov.visibleTargets.Count == 0 || fov.visibleTargets[0] == null && isShooting)
        {
            StopShooting();
        }
    }

    public void StartShooting()
    {
        if (shootingCoroutine == null)
        {
            isShooting = true;
            shootingCoroutine = StartCoroutine(Shoot());
        }
    }

    public void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
            isShooting = false;
        }
    }

    private IEnumerator Shoot()
    {
        while (isShooting)
        {
            if (Physics.Raycast(muzzleTips[muzzleIndex].position, muzzleTips[muzzleIndex].forward, out rayHit, 1000f))
            {
                MuzzleFX();
                //ImpactFX();
                ShotPhysics();
                ShotDamage();
                AudioManager.instance.StartPlayingAtPosition("Tank Shot", muzzleTips[muzzleIndex].position);
            }
            yield return new WaitForSeconds(shotInterval);
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
        poolManager.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.TankShellTrail, muzzle.position, muzzle.rotation, 3f);
        muzzleIndex++;
        if (muzzleIndex >= muzzleTips.Count)
        {
            muzzleIndex = 0;
        }
    }

    //private void ImpactFX()
    //{
    //    poolManager.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.BulletImpact, rayHit.point, Quaternion.Euler(-90,0,0), 2f);
    //}
}
