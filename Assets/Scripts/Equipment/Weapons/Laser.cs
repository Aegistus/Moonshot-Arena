using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour, IWeapon
{
    private LineRenderer line;
    private bool firingLaser = false;

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
            print("firing laser");
        }
    }

    public IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(5f);
        firingLaser = false;
    }

    public IEnumerator Reload()
    {
        yield return null;
    }

    public void StartAttack()
    {
        firingLaser = true;
        PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.ChargingLaser, transform.position, transform.rotation, 5f);
        StartCoroutine(EndAttack());
    }

    public void DisableWeapon()
    {

    }

    public void EnableWeapon()
    {

    }
}
