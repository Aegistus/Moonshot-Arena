﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void StartAttack();
    void EndAttack();
    void Reload();
}
