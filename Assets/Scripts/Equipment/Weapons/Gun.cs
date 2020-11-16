using System;
using System.Collections;
using UnityEngine;

public abstract class Gun : Weapon
{
    public static event Action<int, int> OnAmmoAmountChange;
    public Transform gunTip;

    [HideInInspector]
    public int loadedAmmo;
    [HideInInspector]
    public int carriedAmmo = 100;
    [HideInInspector]
    public bool reloading = false;

    private bool currentWeapon = true;

    protected virtual void Start()
    {
        loadedAmmo = stats.maxAmmo;
        if (currentWeapon)
        {
            OnAmmoAmountChange?.Invoke(loadedAmmo, carriedAmmo);
        }
    }

    protected void Update()
    {
        if (reloading)
        {
            transform.Rotate(-900 * Time.deltaTime, 0, 0);
        }
    }

    public void CheckAmmoAmount()
    {
        OnAmmoAmountChange?.Invoke(loadedAmmo, carriedAmmo);
    }

    public void DoDamage(GameObject hitObject)
    {
        Health toDamage = hitObject.GetComponent<Health>();
        if (toDamage != null)
        {
            toDamage.Damage(stats.damage);
        }
        else
        {
            toDamage = hitObject.GetComponentInParent<Health>();
            if (toDamage != null)
            {
                toDamage.Damage(stats.damage);
                //print("damaging");
            }
        }
    }

    public void UseAmmo()
    {
        loadedAmmo--;
        OnAmmoAmountChange?.Invoke(loadedAmmo, carriedAmmo);
    }

    public void LoadAmmo()
    {
        OnAmmoAmountChange?.Invoke(loadedAmmo, carriedAmmo);
    }

    public void AddAmmoToSupply(int ammoAmount)
    {
        carriedAmmo += ammoAmount;
        if (currentWeapon)
        {
            OnAmmoAmountChange?.Invoke(loadedAmmo, carriedAmmo);
        }
    }

    public override void DisableWeapon()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        currentWeapon = false;
    }

    public override void EnableWeapon()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        OnAmmoAmountChange?.Invoke(loadedAmmo, carriedAmmo);
        currentWeapon = true;
    }
}
