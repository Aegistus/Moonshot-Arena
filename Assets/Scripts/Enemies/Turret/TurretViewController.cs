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
        RespawnManager.OnPlayerRespawn += RetargetNewPlayer;
    }

    private void RetargetNewPlayer(GameObject obj)
    {
        target = obj.transform;
    }

    Quaternion lookRotation;
    private void Update()
    {
        if (target != null)
        {
            lookRotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, swivelSpeed * Time.deltaTime);
        }
    }
}
