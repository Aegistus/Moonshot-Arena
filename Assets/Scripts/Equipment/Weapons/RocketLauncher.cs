using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun
{
    public GameObject rocketPrefab;

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

    public override void StartAttack()
    {
        if (reset && !reloading && loadedAmmo > 0)
        {
            SpawnRocket();
            AudioManager.instance.StartPlayingAtPosition("Rocket Shoot", transform.position);
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

    private void SpawnRocket()
    {
        Instantiate(rocketPrefab, gunTip.position, gunTip.rotation);
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
        pool.GetObjectFromPoolWithLifeTime(stats.bulletTrailTag, gunTip.position + gunTip.forward, gunTip.rotation, 4f);
    }

    public override IEnumerator EndAttack()
    {
        yield return null;
    }

    public override IEnumerator Reload()
    {
        if (!reloading && carriedAmmo > 0 && loadedAmmo < stats.maxAmmo)
        {
            anim.Play("Reload");
            reloading = true;
            yield return new WaitForSeconds(stats.reloadTime);
            int ammoToAdd = carriedAmmo > stats.maxAmmo ? stats.maxAmmo - loadedAmmo : carriedAmmo;
            loadedAmmo += ammoToAdd;
            carriedAmmo -= ammoToAdd;
            LoadAmmo();
            reloading = false;
        }
        else
        {
            print("no more ammo");
        }
    }

    protected override void Update()
    {

    }
}
