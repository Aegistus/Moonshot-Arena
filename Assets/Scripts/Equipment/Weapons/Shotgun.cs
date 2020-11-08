using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun, IWeapon
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
        yield return new WaitForSeconds(3f);
        AddAmmo();
    }

    private RaycastHit rayHit;
    private List<Vector3> trajectories = new List<Vector3>();
    public override void StartAttack()
    {
        if (reset && loadedAmmo > 0)
        {
            trajectories.Clear();
            for (int i = 0; i < pelletCount; i++)
            {
                trajectories.Add(Input.mousePosition + new Vector3((Random.value * shotSpread) - shotSpread / 2, (Random.value * shotSpread) - shotSpread / 2, 2));
            }
            for (int i = 0; i < trajectories.Count; i++)
            {
                if (Physics.Raycast(cam.ScreenPointToRay(trajectories[i]), out rayHit, Mathf.Infinity, stats.targetAbleLayers))
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
                    Debug.DrawRay(cam.ScreenPointToRay(trajectories[i]).origin, cam.ScreenPointToRay(trajectories[i]).direction, Color.red, 100000f);
                }
                GunFX();
                BulletTrails();
            }
            playerRB.velocity += -cam.transform.forward * stats.kickBack;
            anim.Play("Fire");
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
        //anim.Play("Fire");
        pool.GetObjectFromPoolWithLifeTime(stats.muzzleFlashTag, gunTip.position, gunTip.rotation, 2f);
        if (rayHit.point != null)
        {
            pool.GetObjectFromPoolWithLifeTime(stats.bulletImpactTag, rayHit.point, Quaternion.Euler(-90, 0, 0), 2f);
        }
    }

    private void BulletTrails()
    {
        pool.GetObjectFromPoolWithLifeTime(stats.bulletTrailTag, gunTip.position + gunTip.forward, Quaternion.LookRotation(rayHit.point - gunTip.position + gunTip.forward), 4f);
    }
}
