﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartUI : MonoBehaviour
{
    public GameObject gameOverUI;

    private GameObject levelCamera;

    private void Start()
    {
        levelCamera = GameObject.FindGameObjectWithTag("Level Camera");
        levelCamera?.SetActive(false);
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

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Reloading Scene");
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerDeath -= ActivateRespawnScreen;
    }
}
