using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour
{
    public float fovLerpSpeed = 1f;
    public float rotationLerpSpeed = 1f;
    public float bounceSpeed = 3f;
    public float bounceHeightRange = .1f;
    public float horizontalBounceRange = .05f;

    private Camera cam;

    private float targetFOV;
    private float targetZRotation = 0;

    private float startingFOV;

    private bool isBouncing = false;
    private float startingY;
    private float startingX;
    private float bounceTimer = 0;

    private void Start()
    {
        cam = GetComponent<Camera>();
        startingFOV = cam.fieldOfView;
        targetFOV = startingFOV;
        startingY = transform.localPosition.y;
        startingX = transform.localPosition.x;
    }

    bool hitBottom = false;
    private void Update()
    {
        if (cam.fieldOfView != targetFOV)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovLerpSpeed * Time.deltaTime);
        }
        cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, targetZRotation);
        if (isBouncing)
        {
            float camY = cam.transform.localPosition.y + Mathf.Sin(bounceTimer * bounceSpeed) * bounceHeightRange;
            float camX = cam.transform.localPosition.x + Mathf.Sin(bounceTimer * bounceSpeed) * horizontalBounceRange * .5f;
            cam.transform.localPosition = new Vector3(camX, camY, cam.transform.localPosition.z);
            bounceTimer += Time.deltaTime;
            if (camY <= startingY - bounceHeightRange + .01f && !hitBottom)
            {
                AudioManager.instance.StartPlayingAtPosition("Footstep", transform.position);
                print("Footstep");
                hitBottom = true;
            }
            if (camY >= startingY + bounceHeightRange - (bounceHeightRange / 5) && hitBottom)
            {
                hitBottom = false;
            }
        }
    }

    public void BounceHead(bool bounce)
    {
        isBouncing = bounce;
        bounceTimer = 0;
        if (!isBouncing)
        {
            cam.transform.localPosition = new Vector3(startingX, startingY, cam.transform.localPosition.z);
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
