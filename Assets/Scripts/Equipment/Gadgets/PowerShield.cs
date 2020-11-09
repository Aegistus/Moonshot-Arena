using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour, IGadget
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

    public void StartUse()
    {
        player.SetVelocity(player.velocity.normalized);
        model.SetActive(true);
    }

    public void EndUse()
    {
        player.ModifyVelocity(1);
        model.SetActive(false);
    }

    public void DisableGadget()
    {
        gameObject.SetActive(false);
    }

    public void EnableGadget()
    {
        gameObject.SetActive(true);
    }

}
