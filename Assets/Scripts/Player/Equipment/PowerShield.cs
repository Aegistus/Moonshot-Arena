using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour
{
    public float velocityModifier = .5f;

    private PlayerController player;
    private GameObject model;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        model = transform.GetChild(0).gameObject;
        model.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ActivateShield();
        }
        if (Input.GetMouseButtonUp(1))
        {
            DeactivateShield();
        }
    }


    private void ActivateShield()
    {
        player.ModifyVelocity(velocityModifier);
        model.SetActive(true);
    }

    private void DeactivateShield()
    {
        player.ModifyVelocity(1);
        model.SetActive(false);
    }

}
