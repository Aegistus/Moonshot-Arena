using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public int damage = 10;
    public float damageInterval = .5f;
    public float activeInterval = 5f;
    public float resetInterval = 4f;
    public float startingDelay = 0f;

    private List<ParticleSystem> fireFX = new List<ParticleSystem>();
    private List<Collider> colliders = new List<Collider>();
    private List<Health> enteredEntities = new List<Health>();

    private void Start()
    {
        colliders.AddRange(GetComponents<Collider>());
        fireFX.AddRange(GetComponentsInChildren<ParticleSystem>());
        StartCoroutine(DoDamage());
        StartCoroutine(DelayStart());
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(startingDelay);
        StartCoroutine(SpringTrap());
    }

    private IEnumerator SpringTrap()
    {
        while (true)
        {
            foreach (var ps in fireFX)
            {
                ps.Play();
            }
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
            yield return new WaitForSeconds(activeInterval);
            foreach (var ps in fireFX)
            {
                ps.Stop();
            }
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
            enteredEntities.Clear();
            yield return new WaitForSeconds(resetInterval);
        }
    }

    private IEnumerator DoDamage()
    {
        while (true)
        {
            foreach (Health entity in enteredEntities)
            {
                entity.Damage(damage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponentInParent<Health>();
        if (health && !enteredEntities.Contains(health))
        {
            enteredEntities.Add(health);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponentInParent<Health>();
        if (health)
        {
            enteredEntities.Remove(health);
        }
    }

}
