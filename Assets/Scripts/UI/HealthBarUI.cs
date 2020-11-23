using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public Transform healthBar;

    private void Awake()
    {
        PlayerHealth.OnPlayerHealthChange += ChangeHealthDisplay;
    }

    private void ChangeHealthDisplay(int health)
    {
        healthBar.localScale = new Vector3(health / 100f, 1, 1);
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerHealthChange -= ChangeHealthDisplay;
    }
}
