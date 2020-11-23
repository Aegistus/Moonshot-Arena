using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthNumberUI : MonoBehaviour
{
    public TextMeshProUGUI healthNumber;

    private void Awake()
    {
        PlayerHealth.OnPlayerHealthChange += ChangeHealthDisplay;
    }

    private void ChangeHealthDisplay(int health)
    {
        healthNumber.text = health + "";
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerHealthChange -= ChangeHealthDisplay;
    }
}
