using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public static event Action<int> OnPlayerHealthChange;
    public static event Action OnPlayerDeath;

    public override void Damage(int damage)
    {
        base.Damage(damage);
        OnPlayerHealthChange?.Invoke(currentHealth);
    }

    public override void Heal(int healing)
    {
        base.Heal(healing);
        OnPlayerHealthChange?.Invoke(currentHealth);
    }

    public override void Kill()
    {
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
