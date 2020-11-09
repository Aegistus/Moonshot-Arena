using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmo : ItemDrop
{
    public int ammo = 20;

    public override void GiveItem(Collider player)
    {
        Gun pistol = (Gun) player.GetComponent<PlayerAttack>().carriedWeapons.Find(x => x.GetType() == typeof(Pistol));
        if (pistol != null && pistol.GetType() == typeof(Pistol))
        {
            pistol.AddAmmoToSupply(ammo);
            Destroy(gameObject);
        }
    }
}
