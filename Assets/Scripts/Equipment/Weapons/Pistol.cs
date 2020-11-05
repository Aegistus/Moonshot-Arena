using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public WeaponStats stats;

    private Camera cam;
    private Rigidbody playerRB;

    private void Start()
    {
        cam = Camera.main;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private RaycastHit rayHit;
    public override void StartAttack()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, stats.physicsObjectLayer))
        {
            Rigidbody rb = rayHit.collider.GetComponent<Rigidbody>();
            if (rb)
            {
                if (rb.isKinematic)
                {
                    rb.isKinematic = false;
                }
                rb.velocity += cam.transform.forward * stats.bulletForce;
            }
        }
        playerRB.velocity += -cam.transform.forward * stats.kickBack;
    }

    public override void EndAttack()
    {
        
    }

    public override void Reload()
    {
        
    }
}
