using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    public ParticleSystem fireBurst;
    public Collider flameCollider;
    public int damage = 10;
    public float damagePerSecond = 1f;
    public float triggerDelay = .2f;

    private bool firing = false;
    private bool reloading = false;
    private bool damaging = false;
    private AudioSource audioSource;

    private Coroutine damageCoroutine;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fireBurst.Stop();
        flameCollider.enabled = false;
    }

    private void Update()
    {
        if (firing)
        {
            if (damageCoroutine != null && damaging)
            {
                StopCoroutine(damageCoroutine);
                damaging = false;
            }
        }
    }

    PlayerHealth health;
    private void OnTriggerEnter(Collider other)
    {
        print("Flame Hit Something");
        if (other.gameObject.CompareTag("Player"))
        {
            print("Flame hit player");
            health = other.GetComponent<PlayerHealth>();
            damageCoroutine = StartCoroutine(DoDamage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("player exited");
            health = null;
            StopCoroutine(damageCoroutine);
        }
    }

    private IEnumerator DoDamage()
    {
        while (firing)
        {
            health.Damage(damage);
            yield return new WaitForSeconds(damagePerSecond);
        }
        damaging = false;
    }

    public override IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(5f);
        firing = false;
        //audioSource.Stop();
        fireBurst.Stop();
        flameCollider.enabled = false;
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
        if (firing == false && reloading == false)
        {
            firing = true;
            //AudioManager.instance.StartPlayingAtPosition("Laser Start", transform.position);
            //audioSource.Play();
            fireBurst.Play();
            StartCoroutine(FlameTriggerDelay());
            StartCoroutine(EndAttack());
        }
    }

    private IEnumerator FlameTriggerDelay()
    {
        yield return new WaitForSeconds(triggerDelay);
        flameCollider.enabled = true;
    }

    public override void DisableWeapon()
    {

    }

    public override void EnableWeapon()
    {

    }
}
