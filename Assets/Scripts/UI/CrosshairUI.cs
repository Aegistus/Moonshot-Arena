using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    public static CrosshairUI instance;

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
    }

    public Image crosshairImage;

    public void ChangeCrosshair(Sprite newCrosshair)
    {
        crosshairImage.sprite = newCrosshair;
    }
}
