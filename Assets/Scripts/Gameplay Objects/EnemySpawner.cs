using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public Transform spawnPoint;
    public float respawnTime = 30f;

    private void Start()
    {
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
