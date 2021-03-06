﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public float shotSpread = 1f;
    public float pelletCount = 5f;
    public float resetTime = 1f;

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

    public override IEnumerator EndAttack()
    {
        yield return null;
    }

    public override IEnumerator Reload()
    {
        if (!reloading)
        {
            reloading = true;
            while (reloading && carriedAmmo > 0 && loadedAmmo < stats.maxAmmo)
            {
                anim.enabled = false;
                yield return new WaitForSeconds(stats.reloadTime);
                int ammoToAdd = 0;
                if (stats.maxAmmo - loadedAmmo == 1)
                {
                    ammoToAdd = 1;
                }
                else
                {
                    ammoToAdd = 2;
                }
                loadedAmmo += ammoToAdd;
                carriedAmmo -= ammoToAdd;
                LoadAmmo();
                transform.localRotation = Quaternion.identity;
                anim.enabled = true;
                InsertMagazineSound();
            }
        }
        else
        {
            print("no more ammo");
        }
        reloading = false;
        transform.localRotation = Quaternion.identity;
    }

    private RaycastHit rayHit;
    private List<Vector3> trajectories = new List<Vector3>();
    public override void StartAttack()
    {
        if (reset && loadedAmmo > 0)
        {
            if (reloading)
            {
                StopCoroutine(Reload());
                reloading = false;
                transform.localRotation = Quaternion.identity;
                anim.enabled = true;
            }
            trajectories.Clear();
            for (int i = 0; i < pelletCount; i++)
            {
                trajectories.Add(Input.mousePosition + new Vector3((Random.value * shotSpread) - shotSpread / 2, (Random.value * shotSpread) - shotSpread / 2, 2));
            }
            for (int i = 0; i < trajectories.Count; i++)
            {
                if (Physics.Raycast(cam.ScreenPointToRay(trajectories[i]), out rayHit, Mathf.Infinity, stats.targetAbleLayers))
                {
                    //Rigidbody rb = rayHit.collider.GetComponent<Rigidbody>();
                    //if (rb)
                    //{
                    //    if (rb.isKinematic)
                    //    {
                    //        rb.isKinematic = false;
                    //    }
                    //    rb.velocity += cam.transform.forward * stats.bulletForce;
                    //}
                    DoDamage(rayHit.collider.gameObject);
                    Debug.DrawRay(cam.ScreenPointToRay(trajectories[i]).origin, cam.ScreenPointToRay(trajectories[i]).direction, Color.red, 100000f);
                }
                GunFX();
            }
            AudioManager.instance.StartPlayingAtPosition("Shotgun Shot", transform.position);
            playerRB.velocity += -cam.transform.forward * stats.kickBack;
            anim.Play("Fire");
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
        yield return new WaitForSeconds(resetTime);
        reset = true;
    }

    private void GunFX()
    {
        //anim.Play("Fire");
        pool.GetObjectFromPoolWithLifeTime(stats.muzzleFlashTag, gunTip.position, gunTip.rotation, 2f);
        pool.GetObjectFromPoolWithLifeTime(stats.bulletTrailTag, gunTip.position + gunTip.forward, gunTip.rotation, 4f);
    }
}
