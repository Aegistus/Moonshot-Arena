using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapon Stats", order = 1)]
public class WeaponStats : ScriptableObject
{
    public GameObject droppedWeaponPrefab;
    public Sprite crosshair;
    public int damage = 10;
    public float resetTime = .1f;
    public float reloadTime = 2f;
    public int maxAmmo = 10;
    public int startingAmmo = 40;
    public float bulletForce = 20f;
    public float kickBack = 5f;
    public float recoil = 3f;
    public float roundsPerMinute = 60f;
    public LayerMask targetAbleLayers;
    public PoolManager.PoolTag muzzleFlashTag = PoolManager.PoolTag.MuzzleFlash;
    public PoolManager.PoolTag bulletImpactTag = PoolManager.PoolTag.BulletImpact;
    public PoolManager.PoolTag bulletTrailTag = PoolManager.PoolTag.BulletTrail;
}
