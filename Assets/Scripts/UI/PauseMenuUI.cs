using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    public GameObject pauseMenu;

    private PlayerController player;

    private void Start()
    {
        IsPaused = false;
        pauseMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        RespawnManager.OnPlayerRespawn += GetRespawnedPlayer;
    }

    private void GetRespawnedPlayer(GameObject obj)
    {
        player = obj.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = true;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.enabled = false;
        }
    }

    public void ResumeGame()
    {
        IsPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.enabled = true;
    }

    public void ReturnToMainMenu()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
