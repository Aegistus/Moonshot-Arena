using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostIndicatorUI : MonoBehaviour
{
    public List<GameObject> indicators = new List<GameObject>();

    private void Start()
    {
        Booster.OnCooldownChange += ChangeIndicator;
    }

    public void ChangeIndicator(int numOfCharges)
    {
        for (int i = 0; i < indicators.Count; i++)
        {
            if (i < numOfCharges)
            {
                indicators[i].SetActive(true);
            }
            else
            {
                indicators[i].SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        Booster.OnCooldownChange -= ChangeIndicator;
    }
}
