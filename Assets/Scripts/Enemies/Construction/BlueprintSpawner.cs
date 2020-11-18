using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlueprintSpawner : MonoBehaviour
{
    public static event Action<BlueprintEnemy> OnNewBlueprintSpawned;

    public List<GameObject> blueprintPrefabs;
    public float respawnTime = 30f;

    private BlueprintEnemy currentBlueprint;

    private void Start()
    {
        SpawnRandomBlueprint();
        BlueprintEnemy.OnFinishedBlueprint += SpawnNewBlueprint;
    }

    private void SpawnNewBlueprint(BlueprintEnemy obj)
    {
        if (obj == currentBlueprint)
        {
            StartCoroutine(SpawnCountdown());
        }
    }

    private IEnumerator SpawnCountdown()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnRandomBlueprint();
    }

    public void SpawnRandomBlueprint()
    {
        if (currentBlueprint == null)
        {
            currentBlueprint = Instantiate(blueprintPrefabs[(int)(UnityEngine.Random.value * blueprintPrefabs.Count)], transform.position, transform.rotation).GetComponent<BlueprintEnemy>();
            OnNewBlueprintSpawned?.Invoke(currentBlueprint);
        }
    }
}
