using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IEffect
{
    public LayerMask collisionLayers;
    public float radius = 5f;
    public float force = 5f;
    public int damage = 20;

    public CameraShake.Properties shakeProperties;

    Health health;
    RaycastHit[] sphereHits;
    public void StartEffect()
    {
        AudioManager.instance.StartPlayingAtPosition("Explosion", transform.position);
        sphereHits = Physics.SphereCastAll(transform.position, radius, Vector3.one, radius, collisionLayers);
        foreach (var hit in sphereHits)
        {
            health = hit.collider.GetComponentInParent<Health>();
            if (health != null)
            {
                health.Damage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddExplosionForce(force, transform.position, radius);
            }

        }
        sphereHits = Physics.SphereCastAll(transform.position, radius * 2, Vector3.one, radius, collisionLayers);
        foreach (var hit in sphereHits)
        {
            CameraShake shake = hit.collider.GetComponentInChildren<CameraShake>();
            if (shake != null)
            {
                shake.StartShake(shakeProperties);
            }
        }
    }
}
