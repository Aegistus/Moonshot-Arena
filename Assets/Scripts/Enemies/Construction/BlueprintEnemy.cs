using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlueprintEnemy : MonoBehaviour
{
    public static event Action<BlueprintEnemy> FinishedBlueprint;

    public GameObject finishedPrefab;

    private float percentDone = 0;

    public void AddWork(float percent)
    {
        percentDone += percent;
        if (percentDone >= 100)
        {
            FinishConstruction();
        }
    }

    public void FinishConstruction()
    {
        Instantiate(finishedPrefab, transform.position, transform.rotation);
        FinishedBlueprint?.Invoke(this);
        Destroy(gameObject);
    }
}
