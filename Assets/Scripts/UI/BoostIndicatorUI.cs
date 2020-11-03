using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostIndicatorUI : MonoBehaviour
{
    public GameObject indicator;

    private void Start()
    {
        Booster.OnCooldownChange += ChangeIndicator;
    }

    public void ChangeIndicator(bool onCooldown)
    {
        if (onCooldown)
        {
            indicator.SetActive(false);
        }
        else
        {
            indicator.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Booster.OnCooldownChange -= ChangeIndicator;
    }
}
