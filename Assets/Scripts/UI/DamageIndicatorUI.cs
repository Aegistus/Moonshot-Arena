using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorUI : MonoBehaviour
{
    public GameObject indicator;
    public float decaySpeed = 2f;

    private int playerHealth;
    private Image indicatorImage;
    private Color initialColor;

    private void Start()
    {
        indicatorImage = indicator.GetComponentInChildren<Image>();
        initialColor = indicatorImage.color;
        indicator.SetActive(false);
        PlayerHealth.OnPlayerHealthChange += ShowUIWhenDamaged;
    }

    private void ShowUIWhenDamaged(int newHealth)
    {
        if (newHealth < playerHealth)
        {
            indicator.SetActive(true);
            indicatorImage.color = initialColor;
        }
        playerHealth = newHealth;
    }

    Color color;
    private void Update()
    {
        if (indicator.activeInHierarchy)
        {
            float a = Mathf.Lerp(indicatorImage.color.a, 0, decaySpeed * Time.deltaTime);
            color = indicatorImage.color;
            color.a = a;
            indicatorImage.color = color;
        }
        if (indicatorImage.color.a <= 0)
        {
            indicatorImage.color = initialColor;
            indicator.SetActive(false);
        }
    }
}
