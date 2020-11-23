using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Text extraCount;
    public List<GameObject> nubs = new List<GameObject>();

    private void Start()
    {
        Gun.OnAmmoAmountChange += UpdateAmmoDisplay;
    }

    private void UpdateAmmoDisplay(int ammo, int extraAmmo)
    {
        int index = 0;
        while (ammo > index)
        {
            nubs[index].SetActive(true);
            index++;
        }
        while (index < nubs.Count)
        {
            nubs[index].SetActive(false);
            index++;
        }
        extraCount.text = extraAmmo + "";
    }
}
