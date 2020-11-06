using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour, IKillable
{
    [HideInInspector]
    public int currentHealth;
    public readonly int maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void Damage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
    }

    public virtual void Heal(int healing)
    {
        currentHealth = Mathf.Clamp(currentHealth + healing, 0, maxHealth);
    }

    public abstract void Kill();
}
