using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGAmmoDrop : ItemDrop
{
    public int ammo = 40;
    
    public override void GiveItem(Collider player)
    {
        Gun smg = (Gun)player.GetComponent<PlayerAttack>().carriedWeapons.Find(x => x.GetType() == typeof(SMG));
        if (smg != null && smg.GetType() == typeof(SMG))
        {
            smg.AddAmmoToSupply(ammo);
        }
    }
}
