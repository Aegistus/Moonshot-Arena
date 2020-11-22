using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resp : MonoBehaviour
{
    public static event Action<GameObject> OnPlayerRespawn;

    public GameObject gameOverUI;
    public GameObject playerPrefab;

    private GameObject levelCamera;
    private List<GameObject> spawnPoints = new List<GameObject>();

    private void Start()
    {
        levelCamera = GameObject.FindGameObjectWithTag("Level Camera");
        levelCamera.SetActive(false);
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("Respawn"));
        PlayerHealth.OnPlayerDeath += ActivateRespawnScreen;
        gameOverUI.SetActive(false);
    }

    private void ActivateRespawnScreen()
    {
        levelCamera.SetActive(true);
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RespawnPlayer()
    {
        int randIndex = (int)UnityEngine.Random.value * spawnPoints.Count;
        levelCamera.gameObject.SetActive(false);
        gameOverUI.SetActive(false);
        GameObject player = Instantiate(playerPrefab, spawnPoints[randIndex].transform.position, spawnPoints[randIndex].transform.rotation);
        OnPlayerRespawn?.Invoke(player);
    }
}
