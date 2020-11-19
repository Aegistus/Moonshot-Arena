using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CapsuleCollider))]
public class WeaponPickup : MonoBehaviour
{
    public static event Action<string> OnPlayerHasSwapOption;
    public static event Action OnPlayerLeaveWeaponSwap;

    public GameObject weapon;
    public string weaponName = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerHasSwapOption?.Invoke(weaponName);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerAttack attack = other.GetComponent<PlayerAttack>();
            attack.SwapWeapon(weapon, transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerLeaveWeaponSwap?.Invoke();
        }
    }

    public void DestroyPickup()
    {
        OnPlayerLeaveWeaponSwap?.Invoke();
        AudioManager.instance.StartPlaying("Pickup Ammo");
        Destroy(gameObject);
    }
}
