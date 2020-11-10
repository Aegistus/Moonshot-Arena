using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public float lifeTime;

    private ParticleSystem particles;
    private AudioSource audioSource;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnEnable()
    {
        if (particles)
        {
            particles.Play();
        }
        if (audioSource)
        {
            audioSource.Play();
        }
        StartCoroutine(EndLifeTime());
    }

    private IEnumerator EndLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

}
