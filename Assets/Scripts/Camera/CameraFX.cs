using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour
{
    public float fovLerpSpeed = 1f;
    public float rotationLerpSpeed = 1f;

    private Camera cam;

    private float targetFOV;
    private float targetXRotation;

    private float startingFOV;
    private float startingXRotation;

    private void Start()
    {
        cam = GetComponent<Camera>();
        startingFOV = cam.fieldOfView;
        startingXRotation = cam.transform.rotation.eulerAngles.x;
        targetFOV = startingFOV;
        targetXRotation = startingXRotation;
    }

    private void Update()
    {
        if (cam.fieldOfView != targetFOV)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovLerpSpeed * Time.deltaTime);
        }
        //if (cam.transform.rotation.eulerAngles.x != targetXRotation)
        //{
        //    cam.transform.Rotate(new Vector3(Mathf.LerpUnclamped(cam.transform.eulerAngles.x, targetXRotation, rotationLerpSpeed * Time.deltaTime), 0, 0));
        //}
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
        targetXRotation += rotation;
    }

    public void ResetRotation()
    {
        targetXRotation = startingXRotation;
    }
}
