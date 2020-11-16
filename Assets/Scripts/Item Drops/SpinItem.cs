using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinItem : MonoBehaviour
{
    public float spinSpeed = 20f;

    private void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
