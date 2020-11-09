using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    public IWeapon currentWeapon;
    public List<GameObject> weapons;
    public List<IWeapon> carriedWeapons = new List<IWeapon>();

    private void Start()
    {
        foreach (var weapon in weapons)
        {
            carriedWeapons.Add(weapon.GetComponent<IWeapon>());
        }
        currentWeapon = carriedWeapons[0];
        for (int i = 1; i < carriedWeapons.Count; i++)
        {
            carriedWeapons[i].DisableWeapon();
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
}
