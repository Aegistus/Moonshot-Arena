using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float startDelay = 5f;
    public List<GameObject> enemyPrefabs;
    public Transform spawnPoint;
    public float respawnTime = 45f;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    public IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(enemyPrefabs[(int)(Random.value * enemyPrefabs.Count)], spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(respawnTime);
        }
    }
}
