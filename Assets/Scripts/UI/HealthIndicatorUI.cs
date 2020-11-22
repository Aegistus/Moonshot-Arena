using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicatorUI : MonoBehaviour
{
    public GameObject damageIndicator;
    public GameObject healingIndicator;
    public float decaySpeed = 2f;

    private int playerHealth = 9999999;
    private Image damageImage;
    private Image healingImage;
    private Color damageColor;
    private Color healingColor;

    private void Start()
    {
        damageImage = damageIndicator.GetComponentInChildren<Image>();
        healingImage = healingIndicator.GetComponentInChildren<Image>();
        damageColor = damageImage.color;
        healingColor = healingImage.color;
        damageIndicator.SetActive(false);
        healingIndicator.SetActive(false);
        PlayerHealth.OnPlayerHealthChange += ShowUIWhenHealthChange;
    }

    private void ShowUIWhenHealthChange(int newHealth)
    {
        if (newHealth < playerHealth)
        {
            damageIndicator.SetActive(true);
            damageImage.color = damageColor;
        }
        else if (newHealth > playerHealth)
        {
            healingIndicator.SetActive(true);
            healingImage.color = healingColor;
        }
        playerHealth = newHealth;
    }

    Color color;
    private void Update()
    {
        if (damageIndicator.activeInHierarchy)
        {
            float a = Mathf.Lerp(damageImage.color.a, 0, decaySpeed * Time.deltaTime);
            color = damageImage.color;
            color.a = a;
            damageImage.color = color;
        }
        if (damageImage.color.a <= 0)
        {
            damageImage.color = damageColor;
            damageIndicator.SetActive(false);
        }
        if (healingIndicator.activeInHierarchy)
        {
            float a = Mathf.Lerp(healingImage.color.a, 0, decaySpeed * Time.deltaTime);
            color = healingImage.color;
            color.a = a;
            healingImage.color = color;
        }
        if (healingImage.color.a <= 0)
        {
            healingImage.color = healingColor;
            healingIndicator.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerHealthChange -= ShowUIWhenHealthChange;
    }
}
