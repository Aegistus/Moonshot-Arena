using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public List<ParticleSystem> fireFX = new List<ParticleSystem>();
    public float activeInterval = 5f;
    public float resetInterval = 4f;
    public float startingDelay = 0f;

    private void Start()
    {
        fireFX.AddRange(GetComponentsInChildren<ParticleSystem>());
        StartCoroutine(DelayStart());
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
            yield return new WaitForSeconds(activeInterval);
            foreach (var ps in fireFX)
            {
                ps.Stop();
            }
            yield return new WaitForSeconds(resetInterval);
        }
    }
}
