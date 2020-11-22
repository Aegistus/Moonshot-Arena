using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankController : MonoBehaviour
{
    public LayerMask groundLayer;

    private NavMeshAgent navAgent;
    private Transform target;

    private void Start()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, -transform.up, out rayHit, 20f, groundLayer))
        {
            transform.position = rayHit.point;
        }
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = true;
        RetargetPlayer(GameObject.FindGameObjectWithTag("Player"));
        StartCoroutine(HuntPlayer());
        RespawnManager.OnPlayerRespawn += RetargetPlayer;
    }

    private void RetargetPlayer(GameObject newPlayer)
    {
        target = newPlayer.transform;
        StartCoroutine(HuntPlayer());
    }

    private NavMeshHit meshHit;
    private IEnumerator HuntPlayer()
    {
        while (target != null)
        {
            if (!navAgent.isOnNavMesh)
            {
                transform.position = meshHit.position;
            }
            navAgent.SetDestination(target.position);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDestroy()
    {
        RespawnManager.OnPlayerRespawn -= RetargetPlayer;
    }
}
