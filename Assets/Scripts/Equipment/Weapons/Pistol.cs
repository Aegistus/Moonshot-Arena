using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public float resetTime = .5f;

    private bool reset = true;
    private Camera cam;
    private Rigidbody playerRB;
    private PoolManager pool;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        pool = PoolManager.Instance;
        anim = GetComponent<Animator>();
    }

    private RaycastHit rayHit;
    public override void StartAttack()
    {
        if (reset && currentAmmo > 0)
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
            reset = false;
            UseAmmo();
            StartCoroutine(Reset());
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        reset = true;
    }

    private void GunFX()
    {
        anim.Play("Fire");
        pool.GetObjectFromPoolWithLifeTime(stats.muzzleFlashTag, gunTip.position, gunTip.rotation, 2f);
        if (rayHit.point != null)
        {
            pool.GetObjectFromPoolWithLifeTime(stats.bulletImpactTag, rayHit.point, Quaternion.Euler(-90, 0, 0), 2f);
        }
        pool.GetObjectFromPoolWithLifeTime(stats.bulletTrailTag, gunTip.position + gunTip.forward, gunTip.rotation, 4f);
    }

    public override IEnumerator EndAttack()
    {
        yield return null;
    }

    public override IEnumerator Reload()
    {
        anim.enabled = false;
        reloading = true;
        yield return new WaitForSeconds(1f);
        AddAmmo(stats.maxAmmo);
        reloading = false;
        transform.rotation = Quaternion.identity;
        anim.enabled = true;
    }
}
