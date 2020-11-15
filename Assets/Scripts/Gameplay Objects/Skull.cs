using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SphereCollider))]
public class Skull : MonoBehaviour
{
    public static event Action<Skull> OnSkullCollected;

    public int additionalSeconds = 60;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnSkullCollected?.Invoke(this);
            SurvivalTimer.instance.AddTime(additionalSeconds);
            Destroy(gameObject);
        }
    }
}
