﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour, IWeapon
{
    public WeaponStats stats;
    public Transform gunTip;

    [HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    public bool isReloading;

    public abstract IEnumerator EndAttack();
    public abstract IEnumerator Reload();
    public abstract void StartAttack();
}