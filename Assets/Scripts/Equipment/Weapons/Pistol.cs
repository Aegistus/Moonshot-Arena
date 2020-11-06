using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    private Camera cam;
    private Rigidbody playerRB;
    private PoolManager pool;
    private Animator anim;

    private void Start()
    {
        cam = Camera.main;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        pool = PoolManager.Instance;
        anim = GetComponent<Animator>();
    }

    private RaycastHit rayHit;
    public override void StartAttack()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, stats.targetAbleLayers))
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
        GunFX();
        playerRB.velocity += -cam.transform.forward * stats.kickBack;
    }

    private void GunFX()
    {
        anim.Play("Fire");
        pool.GetObjectFromPoolWithLifeTime(stats.muzzleFlashTag, gunTip.position, gunTip.rotation, 2f);
        if (rayHit.point != null)
        {
            pool.GetObjectFromPoolWithLifeTime(stats.bulletImpactTag, rayHit.point, Quaternion.Euler(-90, 0, 0), 2f);
        }
    }

    public override void EndAttack()
    {

    }

    public override void Reload()
    {
        
    }
}
