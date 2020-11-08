using System;
using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour, IWeapon
{
    public static event Action<int, int> OnAmmoAmountChange;
    public WeaponStats stats;
    public Transform gunTip;

    [HideInInspector]
    public int loadedAmmo;
    [HideInInspector]
    public int extraAmmo = 20;
    [HideInInspector]
    public bool reloading = false;

    protected virtual void Start()
    {
        loadedAmmo = stats.maxAmmo;
        OnAmmoAmountChange?.Invoke(loadedAmmo, extraAmmo);
    }

    protected void Update()
    {
        if (reloading)
        {
            transform.Rotate(-900 * Time.deltaTime, 0, 0);
        }
    }

    public abstract IEnumerator EndAttack();
    public abstract IEnumerator Reload();
    public abstract void StartAttack();

    public void UseAmmo()
    {
        loadedAmmo--;
        OnAmmoAmountChange?.Invoke(loadedAmmo, extraAmmo);
    }

    public void AddAmmo()
    {
        OnAmmoAmountChange?.Invoke(loadedAmmo, extraAmmo);
    }
}
