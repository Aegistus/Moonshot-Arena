using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    public GameObject weaponObject;

    private Weapon weapon;

    private void Start()
    {
        weapon = weaponObject.GetComponent<Weapon>();
    }

    public void ChargeAttack()
    {
        weapon.StartAttack();
    }
}
