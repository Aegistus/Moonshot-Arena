using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IEffect
{
    public LayerMask collisionLayers;
    public float radius = 5f;
    public float force = 5f;
    public int damage = 20;

    Health health;
    RaycastHit[] sphereHits;
    public void StartEffect()
    {
        sphereHits = Physics.SphereCastAll(transform.position, radius, Vector3.one, radius, collisionLayers);
        print(sphereHits.Length);
        foreach (var hit in sphereHits)
        {
            health = hit.collider.GetComponentInParent<Health>();
            if (health != null)
            {
                health.Damage(damage);
            }
            hit.rigidbody.AddExplosionForce(force, transform.position, radius);
        }
    }
}
