using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConstructionLaser : MonoBehaviour
{
    private LineRenderer line;
    private bool firingLaser = false;
    private bool reloading = false;
    private AudioSource audioSource;

    public void Start()
    {
        line = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
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

    public  IEnumerator EndConstruct()
    {
        yield return new WaitForSeconds(5f);
        firingLaser = false;
        line.enabled = false;
        audioSource.Stop();
        StartCoroutine(Reload());
    }

    public void EmergencyStop()
    {
        firingLaser = false;
        line.enabled = false;
        audioSource.Stop();
    }

    public  IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(4f);
        reloading = false;
    }

    public  void StartConstruct()
    {
        if (firingLaser == false && reloading == false)
        {
            firingLaser = true;
            line.enabled = true;
            GameObject spark = PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.ConstructionLaser, transform.position, transform.rotation, Vector3.one * .01f, 5f);
            AudioManager.instance.StartPlayingAtPosition("Laser Start", transform.position);
            audioSource.Play();
            spark.transform.parent = transform;
            StartCoroutine(EndConstruct());
        }
    }
}
