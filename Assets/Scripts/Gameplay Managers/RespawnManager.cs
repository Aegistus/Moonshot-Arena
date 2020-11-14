using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnManager : MonoBehaviour
{
    public static event Action<GameObject> OnPlayerRespawn;

    public GameObject respawnUI;
    public GameObject playerPrefab;
    public Camera levelCamera;

    private List<GameObject> respawnPoints = new List<GameObject>();

    private void Start()
    {
        levelCamera.gameObject.SetActive(false);
        respawnPoints.AddRange(GameObject.FindGameObjectsWithTag("Respawn"));
        PlayerHealth.OnPlayerDeath += ActivateRespawnScreen;
        respawnUI.SetActive(false);
    }

    private void ActivateRespawnScreen()
    {
        levelCamera.gameObject.SetActive(true);
        respawnUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RespawnPlayer()
    {
        int randIndex = (int)UnityEngine.Random.value * respawnPoints.Count;
        levelCamera.gameObject.SetActive(false);
        respawnUI.SetActive(false);
        OnPlayerRespawn?.Invoke(Instantiate(playerPrefab, respawnPoints[randIndex].transform.position, respawnPoints[randIndex].transform.rotation));
    }
}
