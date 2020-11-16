using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullManager : MonoBehaviour
{
    public static SkullManager instance;

    public GameObject skullPrefab;
    public int skullSpawnInterval = 30;

    private List<GameObject> skullSpawnPoints = new List<GameObject>();
    private Dictionary<Skull, GameObject> inUseSpawnPoints = new Dictionary<Skull, GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        skullSpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("Skull Spawn"));
        Skull.OnSkullCollected += ReAddSkullSpawn;
    }

    private void ReAddSkullSpawn(Skull skullCollected)
    {
        GameObject spawnPoint;
        inUseSpawnPoints.TryGetValue(skullCollected, out spawnPoint);
        skullSpawnPoints.Add(spawnPoint);
        inUseSpawnPoints.Remove(skullCollected);
    }

    private void Start()
    {
        StartCoroutine(SpawnSkulls());
    }

    public IEnumerator SpawnSkulls()
    {
        while (true)
        {
            yield return new WaitForSeconds(skullSpawnInterval);
            if (skullSpawnPoints.Count > 0)
            {
                GameObject nextSpawn = skullSpawnPoints[(int)(Random.value * skullSpawnPoints.Count)];
                if (nextSpawn != null)
                {
                    Skull newSkull = Instantiate(skullPrefab, nextSpawn.transform.position, nextSpawn.transform.rotation).GetComponent<Skull>();
                    inUseSpawnPoints.Add(newSkull, nextSpawn);
                    skullSpawnPoints.Remove(nextSpawn);
                }
            }
        }
    }
}
