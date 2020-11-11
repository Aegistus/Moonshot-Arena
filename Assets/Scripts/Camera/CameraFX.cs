using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour
{
    public float fovLerpSpeed = 1f;
    public float rotationLerpSpeed = 1f;

    private Camera cam;

    private float targetFOV;
    private float targetZRotation = 0;

    private float startingFOV;
    private float startingZRotation;

    private void Start()
    {
        cam = GetComponent<Camera>();
        startingFOV = cam.fieldOfView;
        targetFOV = startingFOV;
    }

    private void Update()
    {
        if (cam.fieldOfView != targetFOV)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovLerpSpeed * Time.deltaTime);
        }
        cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, targetZRotation);
    }

    public void AddTargetFOV(float target)
    {
        targetFOV += target;
    }

    public void ResetFOV()
    {
        targetFOV = startingFOV;
    }

    public void AddTargetRotation(float rotation)
    {
        targetZRotation += rotation;
    }

    public void SetTargetRotation(float rotation)
    {
        targetZRotation = rotation;
    }

    public void ResetRotation()
    {
        targetZRotation = 0;
    }
}
