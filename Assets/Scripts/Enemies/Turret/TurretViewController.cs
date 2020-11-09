using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretViewController : MonoBehaviour
{
    public float swivelSpeed = .1f;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    Quaternion lookRotation;
    private void Update()
    {
        lookRotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, swivelSpeed * Time.deltaTime);
    }
}
