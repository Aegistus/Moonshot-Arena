using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour, IWeapon
{
    [HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    public bool isReloading;

    public abstract void EndAttack();
    public abstract void Reload();
    public abstract void StartAttack();
}
