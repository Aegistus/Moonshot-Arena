using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBob : MonoBehaviour
{
    public Transform droneModel;
    public float amplitude = 1f;
    public float frequency = 1f;

    private float timeOffset;

    private void Start()
    {
        timeOffset = Random.value * 2;
    }

    private void Update()
    {
        droneModel.Translate(Vector3.up * (amplitude * Mathf.Sin(frequency * (Time.time - timeOffset))));
    }
}
