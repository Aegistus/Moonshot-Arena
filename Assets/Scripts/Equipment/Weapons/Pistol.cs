using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
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
        if (reset && !reloading && loadedAmmo > 0)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, stats.targetAbleLayers))
            {
                Rigidbody rb = rayHit.collider.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.velocity += cam.transform.forward * stats.bulletForce;
                }
                DoDamage(rayHit.collider.gameObject);
            }
            AudioManager.instance.StartPlayingAtPosition("Gun Shot 01", transform.position);
            GunFX();
            playerRB.velocity += -cam.transform.forward * stats.kickBack;
            reset = false;
            UseAmmo();
            ApplyRecoil();
            StartCoroutine(Reset());
        }
        else if (loadedAmmo == 0)
        {
            AudioManager.instance.StartPlayingAtPosition("Gun Empty Click", transform.position);
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(stats.resetTime);
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
        pool.GetObjectFromPoolWithLifeTime(stats.shellEject, ejectPoint.position, ejectPoint.rotation, 5f);
    }

    public override IEnumerator EndAttack()
    {
        yield return null;
    }

    public override IEnumerator Reload()
    {
        if (!reloading && carriedAmmo > 0 && loadedAmmo < stats.maxAmmo)
        {
            anim.enabled = false;
            reloading = true;
            yield return new WaitForSeconds(stats.reloadTime);
            int ammoToAdd = carriedAmmo > stats.maxAmmo ? stats.maxAmmo - loadedAmmo : carriedAmmo;
            loadedAmmo += ammoToAdd;
            carriedAmmo -= ammoToAdd;
            LoadAmmo();
            reloading = false;
            transform.rotation = Quaternion.identity;
            anim.enabled = true;
        }
        else
        {
            print("no more ammo");
        }
    }
}
