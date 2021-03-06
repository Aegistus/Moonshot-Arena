﻿using System;
using System.Collections;
using UnityEngine;

public abstract class Gun : Weapon
{
    public static event Action<int, int> OnAmmoAmountChange;
    public Transform gunTip;
    public Transform ejectPoint;

    [HideInInspector]
    public int loadedAmmo;
    [HideInInspector]
    public int carriedAmmo;
    [HideInInspector]
    public bool reloading = false;

    private bool currentWeapon = true;
    private CameraShake camShake;

    protected virtual void Start()
    {
        loadedAmmo = stats.maxAmmo;
        carriedAmmo = stats.startingAmmo;
        if (currentWeapon)
        {
            OnAmmoAmountChange?.Invoke(loadedAmmo, carriedAmmo);
        }
        camShake = GetComponentInParent<CameraShake>();
    }

    protected virtual void Update()
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
        if (loadedAmmo > stats.maxAmmo)
        {
            loadedAmmo = stats.maxAmmo;
        }
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
        CrosshairUI.instance.ChangeCrosshair(stats.crosshair);
    }

    public void ApplyRecoil()
    {
        camShake?.StartShake(stats.recoil);
    }

    public void InsertMagazineSound()
    {
        AudioManager.instance.StartPlayingAtPosition("Insert Magazine", transform.position);
    }

    public void RemoveMagazineSound()
    {
        AudioManager.instance.StartPlayingAtPosition("Remove Magazine", transform.position);
    }
}
