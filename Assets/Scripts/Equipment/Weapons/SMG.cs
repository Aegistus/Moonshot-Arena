using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Gun
{
    private bool firing = false;
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

    protected override void Update()
    {
        
    }

    public override IEnumerator EndAttack()
    {
        anim.Play("Idle");
        if (firing == true)
        {
            AudioManager.instance.StopPlaying("Machine Gun Fire");
            AudioManager.instance.StartPlaying("Machine Gun Stop");
        }
        firing = false;
        StopCoroutine(Shoot());
        yield return null;
    }

    public override IEnumerator Reload()
    {
        if (!reloading && carriedAmmo > 0 && loadedAmmo < stats.maxAmmo && !firing)
        {
            anim.Play("Reload");
            reloading = true;
            yield return new WaitForSeconds(stats.reloadTime);
            int ammoToAdd = carriedAmmo > stats.maxAmmo - loadedAmmo ? stats.maxAmmo - loadedAmmo : carriedAmmo;
            loadedAmmo += ammoToAdd;
            carriedAmmo -= ammoToAdd;
            LoadAmmo();
            reloading = false;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            print("no more ammo");
        }
    }

    RaycastHit rayHit;
    public override void StartAttack()
    {
        if (!reloading && loadedAmmo > 0)
        {
            firing = true;
            StartCoroutine(Shoot());
            AudioManager.instance.StartPlaying("Machine Gun Fire");
        }
        else if (loadedAmmo == 0)
        {
            AudioManager.instance.StartPlayingAtPosition("Gun Empty Click", transform.position);
        }
    }

    Rigidbody rb;
    public IEnumerator Shoot()
    {
        while (firing && loadedAmmo > 0 && !reloading)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, stats.targetAbleLayers))
            {
                rb = rayHit.collider.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.velocity += cam.transform.forward * stats.bulletForce;
                }
                DoDamage(rayHit.collider.gameObject);
            }
            GunFX();
            playerRB.velocity += -cam.transform.forward * stats.kickBack;
            firing = true;
            UseAmmo();
            rb = null;
            yield return new WaitForSeconds(stats.resetTime);
        }
        StartCoroutine(EndAttack());
    }

    private void GunFX()
    {
        //anim.Play("Fire");
        pool.GetObjectFromPoolWithLifeTime(stats.muzzleFlashTag, gunTip.position, gunTip.rotation, 2f);
        //if (rayHit.point != null)
        //{
        //    pool.GetObjectFromPoolWithLifeTime(stats.bulletImpactTag, rayHit.point, Quaternion.Euler(-90, 0, 0), 2f);
        //}
        pool.GetObjectFromPoolWithLifeTime(stats.bulletTrailTag, gunTip.position + gunTip.forward, gunTip.rotation, 4f);
    }

    private void OnDestroy()
    {
        AudioManager.instance.StopPlaying("Machine Gun Fire");
        AudioManager.instance.StartPlaying("Machine Gun Stop");
        StopAllCoroutines();
    }
}
