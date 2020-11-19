using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunShake : MonoBehaviour
{
    public CameraShake.Properties shakeSettings;

    private CameraShake shake;

    private void Awake()
    {
        shake = GetComponent<CameraShake>();
    }

    public void StartRunShake()
    {
        shake.StartShake(shakeSettings);
    }
}
