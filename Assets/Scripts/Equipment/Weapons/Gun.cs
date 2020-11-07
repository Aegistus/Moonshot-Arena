using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour, IWeapon
{
    public static event Action<int> OnAmmoAmountChange;
    public WeaponStats stats;
    public Transform gunTip;

    [HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    public bool reloading = false;

    protected virtual void Start()
    {
        OnAmmoAmountChange?.Invoke(currentAmmo);
        currentAmmo = stats.maxAmmo;
    }

    protected void Update()
    {
        if (reloading)
        {
            transform.Rotate(-720 * Time.deltaTime, 0, 0);
        }
    }

    public abstract IEnumerator EndAttack();
    public abstract IEnumerator Reload();
    public abstract void StartAttack();

    public void UseAmmo()
    {
        currentAmmo--;
        OnAmmoAmountChange?.Invoke(currentAmmo);
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, stats.maxAmmo);
        OnAmmoAmountChange?.Invoke(currentAmmo);
    }
}
