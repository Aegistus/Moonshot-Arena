using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    public GameObject weaponObject;

    private IWeapon weapon;

    private void Start()
    {
        weapon = weaponObject.GetComponent<IWeapon>();
    }

    public void ChargeAttack()
    {
        weapon.StartAttack();
    }
}
