using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBot : MonoBehaviour
{
    public Transform rainSoundPosition;
    public float rotationSpeed = 1f;

    private void Start()
    {
        AudioManager.instance.StartPlayingAtPosition("Rain Drop", rainSoundPosition.position, true);
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
