using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlueprintEnemy : MonoBehaviour
{
    public static event Action<BlueprintEnemy> FinishedBlueprint;

    public GameObject finishedPrefab;
    public Material blueprintMat;

    private float percentDone = 0;
    private List<MeshRenderer> meshRends = new List<MeshRenderer>();

    Color color;

    private void Start()
    {
        meshRends.AddRange(GetComponentsInChildren<MeshRenderer>());
        foreach (var mesh in meshRends)
        {
            mesh.material = blueprintMat;
            color = mesh.material.color;
            color.a = 0;
            mesh.material.color = color;
        }
    }

    public void AddWork(float percent)
    {
        percentDone += percent;
        foreach (var mesh in meshRends)
        {
            color = mesh.material.color;
            color.a = percentDone / 1000;
            mesh.material.color = color;
        }
        if (percentDone >= 100)
        {
            FinishConstruction();
        }
    }

    public void FinishConstruction()
    {
        Instantiate(finishedPrefab, transform.position, transform.rotation);
        FinishedBlueprint?.Invoke(this);
        Destroy(gameObject);
    }
}
