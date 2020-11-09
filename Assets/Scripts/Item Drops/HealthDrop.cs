using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : ItemDrop
{
    public int healingAmount = 40;

    public override void GiveItem(Collider player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(healingAmount);
            Destroy(gameObject);
        }
    }
}
