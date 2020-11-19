using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankController : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Transform target;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        RetargetPlayer(GameObject.FindGameObjectWithTag("Player"));
        StartCoroutine(HuntPlayer());
        RespawnManager.OnPlayerRespawn += RetargetPlayer;
    }

    private void RetargetPlayer(GameObject newPlayer)
    {
        target = newPlayer.transform;
        StartCoroutine(HuntPlayer());
    }

    private IEnumerator HuntPlayer()
    {
        while (target != null)
        {
            navAgent.SetDestination(target.position);
            yield return new WaitForSeconds(1);
        }
    }
}
