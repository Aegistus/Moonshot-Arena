using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void StartAttack();
    IEnumerator EndAttack();
    IEnumerator Reload();
}
