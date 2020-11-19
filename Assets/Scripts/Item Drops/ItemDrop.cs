using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public abstract class ItemDrop : MonoBehaviour
{
    public float respawnTime = 60f;

    protected bool spawned = true;
    protected List<GameObject> children = new List<GameObject>();

    protected void Start()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && spawned)
        {
            GiveItem(other);
            spawned = false;
            foreach (var child in children)
            {
                child.SetActive(false);
            }
            StartCoroutine(RespawnItem());
        }
    }

    protected IEnumerator RespawnItem()
    {
        yield return new WaitForSeconds(respawnTime);
        spawned = true;
        foreach (var child in children)
        {
            child.SetActive(true);
        }
    }

    public abstract void GiveItem(Collider player);
}
