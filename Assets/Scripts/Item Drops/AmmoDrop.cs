using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : ItemDrop
{
    public override void GiveItem(Collider player)
    {
        PlayerAttack attack = player.GetComponent<PlayerAttack>();
        foreach (Gun weapon in attack.carriedWeapons)
        {
            if (weapon.GetType() == typeof(Pistol))
            {
                weapon.AddAmmoToSupply(40);
            }
            else if (weapon.GetType() == typeof(SMG))
            {
                weapon.AddAmmoToSupply(60);
            }
            else if (weapon.GetType() == typeof(Shotgun))
            {
                weapon.AddAmmoToSupply(24);
            }
            else if (weapon.GetType() == typeof(LMG))
            {
                weapon.AddAmmoToSupply(40);
            }
        }
        AudioManager.instance.StartPlaying("Pickup Ammo");
    }
}
