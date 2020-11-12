using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponStats stats;

    public abstract void StartAttack();
    public abstract IEnumerator EndAttack();
    public abstract IEnumerator Reload();
    public abstract void DisableWeapon();
    public abstract void EnableWeapon();
}
