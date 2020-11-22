using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonUI : MonoBehaviour
{
    public string connectedLevel;

    public void StartLevel()
    {
        SceneManager.LoadScene(connectedLevel);
    }
}
