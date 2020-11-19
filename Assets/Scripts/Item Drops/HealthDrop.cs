using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : ItemDrop
{
    public int healingAmount = 50;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && spawned)
        {
            GiveItem(other);
        }
    }

    public override void GiveItem(Collider player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null && health.currentHealth < health.maxHealth)
        {
            health.Heal(healingAmount);
            foreach (var child in children)
            {
                child.SetActive(false);
                spawned = false;
                AudioManager.instance.StartPlaying("Heal");
                StartCoroutine(RespawnItem());
            }
        }
    }
}
