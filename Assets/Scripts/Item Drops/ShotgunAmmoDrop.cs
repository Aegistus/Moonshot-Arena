using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmoDrop : ItemDrop
{
    public int ammo = 16;

    public override void GiveItem(Collider player)
    {
        Gun shotgun = (Gun)player.GetComponent<PlayerAttack>().carriedWeapons.Find(x => x.GetType() == typeof(Shotgun));
        if (shotgun != null && shotgun.GetType() == typeof(Shotgun))
        {
            shotgun.AddAmmoToSupply(ammo);
        }
    }
}
