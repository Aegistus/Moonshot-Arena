using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : Turret
{
    public GameObject missilePrefab;
    public float shotInterval = 7f;
    public float targetKnockback = 1f;
    public float rotationSpeed = 5f;
    public Transform coreTransform;
    public List<Transform> missileSilos = new List<Transform>();

    [HideInInspector]
    public bool isShooting = false;

    private PoolManager poolManager;
    private int siloIndex = 0;

    private Coroutine activeFiring;

    protected override void Start()
    {
        base.Start();
        poolManager = PoolManager.Instance;
    }

    private void LateUpdate()
    {
        if (isShooting)
        {
            coreTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (playerSeen && !isShooting)
        {
            StartShooting();
        }
        else if (!playerSeen && isShooting)
        {
            StopShooting();
        }
    }

    public void StartShooting()
    {
        isShooting = true;
        if (activeFiring != null)
        {
            StopCoroutine(activeFiring);
        }
        activeFiring = StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        isShooting = false;
        StopCoroutine(activeFiring);
    }

    private IEnumerator Shoot()
    {
        while (isShooting)
        {
            if (Physics.Raycast(missileSilos[siloIndex].position, missileSilos[siloIndex].forward, 1000f))
            {
                SpawnMissile();
                BackBlastFX();
                AudioManager.instance.StartPlayingAtPosition("Rocket Shoot", missileSilos[siloIndex].position);
            }
            yield return new WaitForSeconds(shotInterval);
        }
    }

    private void SpawnMissile()
    {
        Transform silo = missileSilos[siloIndex];
        Missile missile = Instantiate(missilePrefab, silo.position + transform.forward, transform.rotation, silo).GetComponent<Missile>();
        missile.target = player;
    }

    private void BackBlastFX()
    {
        Transform silo = missileSilos[siloIndex];
        poolManager.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.BackBlast, silo.position, transform.rotation, 5f);
        siloIndex++;
        if (siloIndex >= missileSilos.Count)
        {
            siloIndex = 0;
        }
    }
}
