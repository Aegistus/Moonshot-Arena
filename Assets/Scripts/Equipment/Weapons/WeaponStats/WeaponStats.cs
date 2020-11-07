using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapon Stats", order = 1)]
public class WeaponStats : ScriptableObject
{
    public int maxAmmo = 10;
    public float bulletForce = 20f;
    public float kickBack = 5f;
    public float roundsPerMinute = 60f;
    public LayerMask targetAbleLayers;
    public PoolManager.PoolTag muzzleFlashTag = PoolManager.PoolTag.MuzzleFlash;
    public PoolManager.PoolTag bulletImpactTag = PoolManager.PoolTag.BulletImpact;
    public PoolManager.PoolTag bulletTrailTag = PoolManager.PoolTag.BulletTrail;
}
