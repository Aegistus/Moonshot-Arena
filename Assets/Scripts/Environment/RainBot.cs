using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBot : MonoBehaviour
{
    public Transform rainSoundPosition;
    public float rotationSpeed = 1f;

    // private AudioManager audioManager;

    // private void Start()
    // {
    //     audioManager = AudioManager.instance;
    //     //StartCoroutine(PlayRainSound());
    // }

    // private IEnumerator PlayRainSound()
    // {
    //     float time = .1f;
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(time);
    //         audioManager.StartPlayingAtPosition("Rain Drop", rainSoundPosition.position);
    //         time = Random.value * .2f;
    //     }
    // }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
