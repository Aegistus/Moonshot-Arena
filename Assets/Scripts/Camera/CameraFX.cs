using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour
{
    public float fovLerpSpeed = 1f;
    public float rotationLerpSpeed = 1f;
    public float bounceSpeed = 3f;
    public float bounceHeightRange = .1f;

    private Camera cam;

    private float targetFOV;
    private float targetZRotation = 0;

    private float startingFOV;
    private float startingZRotation;

    private bool isBouncing = false;
    private float startingHeight;
    private float bounceTimer = 0;

    private void Start()
    {
        cam = GetComponent<Camera>();
        startingFOV = cam.fieldOfView;
        targetFOV = startingFOV;
        startingHeight = transform.localPosition.y;
    }

    private void Update()
    {
        if (cam.fieldOfView != targetFOV)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovLerpSpeed * Time.deltaTime);
        }
        cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, targetZRotation);
        if (isBouncing)
        {
            float camY = Mathf.Clamp(cam.transform.localPosition.y + Mathf.Sin(bounceTimer * bounceSpeed) * bounceHeightRange, startingHeight - bounceHeightRange, startingHeight + bounceHeightRange);
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, camY, cam.transform.localPosition.z);
            bounceTimer += Time.deltaTime;
        }
    }

    public void BounceHead(bool bounce)
    {
        isBouncing = bounce;
        bounceTimer = 0;
        if (!isBouncing)
        {
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, startingHeight, cam.transform.localPosition.z);
        }
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
