using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI extraCount;

    private void Start()
    {
        Gun.OnAmmoAmountChange += UpdateAmmoDisplay;
    }

    private void UpdateAmmoDisplay(int ammo, int extraAmmo)
    {
        ammoCount.text = ammo + "";
        extraCount.text = extraAmmo + "";
    }
}
