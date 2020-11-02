﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 1f;

    private Transform camTransform;
    private float xRotation;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camTransform = Camera.main.transform;
    }

    float mouseX;
    float mouseY;
    private void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
