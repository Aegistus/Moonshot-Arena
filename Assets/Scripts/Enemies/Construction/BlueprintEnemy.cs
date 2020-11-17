using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintEnemy : MonoBehaviour
{
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
        Destroy(gameObject);
    }
}
