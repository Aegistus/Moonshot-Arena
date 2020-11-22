using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuUI : MonoBehaviour
{
    public GameObject menu;

    private void Start()
    {
        menu.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        menu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        menu.SetActive(false);
    }
}
