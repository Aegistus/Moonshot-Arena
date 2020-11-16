using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
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
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(respawnTime);
        }
    }
}
