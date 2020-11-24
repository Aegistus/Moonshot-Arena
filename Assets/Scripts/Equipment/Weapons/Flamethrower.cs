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
    [Header("Flame Spread")]
    public float spreadRange = 15f;
    public float randomOffsetRadius = 1f;
    public LayerMask flameableLayers;

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
        if (other.gameObject.CompareTag("Player"))
        {
            health = other.GetComponent<PlayerHealth>();
            damageCoroutine = StartCoroutine(DoDamage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health = null;
            StopCoroutine(damageCoroutine);
        }
    }

    private IEnumerator DoDamage()
    {
        while (firing)
        {
            if (health != null)
            {
                health.Damage(damage);
            }
            yield return new WaitForSeconds(damagePerSecond);
        }
        damaging = false;
    }

    public override IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(4.5f);
        firing = false;
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
            audioSource.Play();
            fireBurst.Play();
            StartCoroutine(FlameTriggerDelay());
            StartCoroutine(EndAttack());
        }
    }

    private IEnumerator FlameTriggerDelay()
    {
        yield return new WaitForSeconds(triggerDelay);
        flameCollider.enabled = true;
        //StartCoroutine(SpreadFlame());
    }

    //RaycastHit rayHit;
    //private IEnumerator SpreadFlame()
    //{
    //    while (firing)
    //    {
    //        yield return new WaitForSeconds(Random.value);
    //        if (Physics.Raycast(transform.position, transform.forward, out rayHit, spreadRange, flameableLayers, QueryTriggerInteraction.Ignore))
    //        {
    //            PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Flame, rayHit.point, Quaternion.identity, 4f);
    //        }
    //    }
    //}

    public override void DisableWeapon()
    {

    }

    public override void EnableWeapon()
    {

    }
}
