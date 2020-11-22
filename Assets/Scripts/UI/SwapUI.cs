using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapUI : MonoBehaviour
{
    public TextMeshProUGUI swapText;

    private void Start()
    {
        WeaponPickup.OnPlayerHasSwapOption += ShowWeaponSwapMessage;
        WeaponPickup.OnPlayerLeaveWeaponSwap += HideWeaponSwapMessage;
        swapText.gameObject.SetActive(false);
    }

    private void HideWeaponSwapMessage()
    {
        swapText.gameObject.SetActive(false);
    }

    private void ShowWeaponSwapMessage(string obj)
    {
        swapText.text = "Hold 'E' to swap for " + obj;
        swapText.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        WeaponPickup.OnPlayerHasSwapOption -= ShowWeaponSwapMessage;
        WeaponPickup.OnPlayerLeaveWeaponSwap -= HideWeaponSwapMessage;
    }

}