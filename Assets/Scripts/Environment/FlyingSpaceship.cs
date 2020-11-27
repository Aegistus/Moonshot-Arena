using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSpaceship : MonoBehaviour
{
    public float shipSpeed = 5f;
    public float resetTime = 20f;

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
        StartCoroutine(ResetPosition());
    }

    private void Update()
    {
        transform.Translate(0, 0, shipSpeed * Time.deltaTime);
    }

    private IEnumerator ResetPosition()
    {
        while(true)
        {
            yield return new WaitForSeconds(resetTime);
            transform.position = startingPosition;
        }
    }
}
