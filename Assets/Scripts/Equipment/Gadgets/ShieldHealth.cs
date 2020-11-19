using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : Health
{
    public override void Kill()
    {
        Destroy(gameObject);
    }
}
