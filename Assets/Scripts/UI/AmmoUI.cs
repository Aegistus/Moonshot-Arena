using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoCount;

    private void Start()
    {
        Gun.OnAmmoAmountChange += UpdateAmmoDisplay;
    }

    private void UpdateAmmoDisplay(int ammo)
    {
        ammoCount.text = ammo + "";
    }
}
