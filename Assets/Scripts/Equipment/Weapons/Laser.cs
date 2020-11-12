using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Laser : Weapon
{
    private LineRenderer line;
    private bool firingLaser = false;
    private bool reloading = false;

    public void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    RaycastHit rayHit;
    private void Update()
    {
        if (firingLaser)
        {
            Physics.Raycast(transform.position, transform.forward, out rayHit);
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, rayHit.point);
        }
    }

    public override IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(5f);
        firingLaser = false;
        line.enabled = false;
        StartCoroutine(Reload());
    }

    public override IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(4f);
        reloading = false;
    }

    public override void StartAttack()
    {
        if (firingLaser == false && reloading == false)
        {
            firingLaser = true;
            line.enabled = true;
            GameObject spark = PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.ChargingLaser, transform.position, transform.rotation, Vector3.one * .01f, 5f);
            spark.transform.parent = transform;
            StartCoroutine(EndAttack());
        }
    }

    public override void DisableWeapon()
    {

    }

    public override void EnableWeapon()
    {

    }
}
