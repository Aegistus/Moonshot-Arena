﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    public Transform handLocation;
    public Weapon currentWeapon;
    public List<GameObject> weapons;
    public List<Weapon> carriedWeapons = new List<Weapon>();

    private bool wantsToSwap = false;
    private float holdTimer = 0f;
    private float maxHoldTimer = 1f;

    private void Start()
    {
        foreach (var weapon in weapons)
        {
            carriedWeapons.Add(weapon.GetComponent<Weapon>());
        }
        currentWeapon = carriedWeapons[0];
        for (int i = 1; i < carriedWeapons.Count; i++)
        {
            carriedWeapons[i].DisableWeapon();
        }
        if (currentWeapon.GetType() == typeof(Gun))
        {
            Gun gun = (Gun)currentWeapon;
            gun.CheckAmmoAmount();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.StartAttack();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(currentWeapon.EndAttack());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(currentWeapon.Reload());
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToWeapon(3);
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            SwitchToWeapon(carriedWeapons.IndexOf(currentWeapon) + 1);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            SwitchToWeapon(carriedWeapons.IndexOf(currentWeapon) - 1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            holdTimer = maxHoldTimer;
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (holdTimer > 0)
            {
                holdTimer -= Time.deltaTime;
            }
            else
            {
                wantsToSwap = true;
                holdTimer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            wantsToSwap = false;
        }
    }

    private void SwitchToWeapon(int index)
    {
        if (index < 0)
        {
            index = carriedWeapons.Count - 1;
        }
        else if (index >= carriedWeapons.Count)
        {
            index = 0;
        }
        currentWeapon.DisableWeapon();
        currentWeapon = carriedWeapons[index];
        currentWeapon.EnableWeapon();
    }

    public void SwapWeapon(GameObject newWeaponPrefab, Transform weaponPickup)
    {
        if (wantsToSwap)
        {
            wantsToSwap = false;
            holdTimer = maxHoldTimer;
            carriedWeapons.Remove(currentWeapon);
            Instantiate(currentWeapon.stats.droppedWeaponPrefab, weaponPickup.position, Quaternion.identity);
            Destroy(currentWeapon.gameObject);

            Weapon newWeapon = Instantiate(newWeaponPrefab, handLocation.position, handLocation.rotation, handLocation).GetComponent<Weapon>();
            carriedWeapons.Add(newWeapon);
            currentWeapon = newWeapon;

            Destroy(weaponPickup.gameObject);
        }
    }
}
