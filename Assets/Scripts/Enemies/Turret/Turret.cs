using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    protected bool playerSeen = false;

    private Transform player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(CheckLineOfSight());
    }

    RaycastHit rayHit;
    Vector3 lookRotation;
    public IEnumerator CheckLineOfSight()
    {
        while (true)
        {
            if (player != null)
            {
                lookRotation = player.position - transform.position;
                if (Physics.Raycast(new Ray(transform.position, lookRotation), out rayHit, 100f))
                {
                    if (rayHit.transform.CompareTag("Player"))
                    {
                        playerSeen = true;
                        print("Player seen");
                    }
                    else
                    {
                        playerSeen = false;
                    }

                }
                else
                {
                    playerSeen = false;
                }
            }
            yield return new WaitForSeconds(.5f);
        }
    }

}
