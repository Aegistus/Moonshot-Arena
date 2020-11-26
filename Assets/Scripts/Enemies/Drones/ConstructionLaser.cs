using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConstructionLaser : MonoBehaviour
{
    public ParticleSystem laserBurst;

    private LineRenderer line;
    private bool firingLaser = false;
    private bool reloading = false;
    private AudioSource audioSource;

    public void Start()
    {
        line = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
        laserBurst.Stop();
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
        laserBurst.Stop();
        StartCoroutine(Reload());
    }

    public void EmergencyStop()
    {
        firingLaser = false;
        line.enabled = false;
        laserBurst.Stop();
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
            AudioManager.instance.StartPlayingAtPosition("Laser Start", transform.position);
            audioSource.Play();
            laserBurst.Play();
            StartCoroutine(EndConstruct());
        }
    }
}
