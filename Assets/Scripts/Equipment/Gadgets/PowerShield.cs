using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour
{
    public float shieldTimeLength = 10f;
    public GameObject shieldPrefab;

    private GameObject currentShield;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartUse();
            StartCoroutine(EndUse());
        }
    }

    public void StartUse()
    {
        if (currentShield == null)
        {
            currentShield = Instantiate(shieldPrefab, transform.position + transform.forward, transform.rotation);
        }
    }

    public IEnumerator EndUse()
    {
        yield return new WaitForSeconds(shieldTimeLength);
        if (currentShield != null)
        {
            Health health = currentShield.GetComponent<Health>();
            health.Kill();
            currentShield = null;
        }
    }
}
