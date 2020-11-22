using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Laser : Weapon
{
    public ParticleSystem laserBurst;
    public int damage = 1;
    public float damagePerSecond = .5f;

    private LineRenderer line;
    private bool firingLaser = false;
    private bool reloading = false;
    private bool damaging = false;
    private AudioSource audioSource;

    private Coroutine damageCoroutine;

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
            if (rayHit.collider != null && rayHit.collider.CompareTag("Player") && !damaging)
            {
                damageCoroutine = StartCoroutine(DoDamage(rayHit.collider.gameObject));
                damaging = true;
            }
            else if (damageCoroutine != null && damaging)
            {
                StopCoroutine(damageCoroutine);
                damaging = false;
            }
        }
    }

    private IEnumerator DoDamage(GameObject toDamage)
    {
        while (firingLaser)
        {
            Health playerHealth = toDamage.GetComponent<Health>();
            playerHealth.Damage(damage);
            yield return new WaitForSeconds(damagePerSecond);
        }
        damaging = false;
    }

    public override IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(5f);
        firingLaser = false;
        line.enabled = false;
        audioSource.Stop();
        laserBurst.Stop();
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
            AudioManager.instance.StartPlayingAtPosition("Laser Start", transform.position);
            audioSource.Play();
            laserBurst.Play();
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
