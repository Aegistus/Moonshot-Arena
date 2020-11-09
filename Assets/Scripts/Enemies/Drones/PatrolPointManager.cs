using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointManager : MonoBehaviour
{
    public static PatrolPointManager current;

    public List<Transform> patrolPoints = new List<Transform>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                patrolPoints.Add(child);
            }
        }
    }

    public Transform GetRandomPointCloseToArea(Vector3 position, float maxRadius, Transform exclude)
    {
        List<Transform> validPoints = new List<Transform>();
        foreach (var point in patrolPoints)
        {
            if (Vector3.Distance(position, point.position) < maxRadius && point != exclude)
            {
                validPoints.Add(point);
            }
        }
        if (validPoints.Count > 0)
        {
            return validPoints[(int)(Random.value * validPoints.Count)];
        }
        else
        {
            return null;
        }
    }

    public Transform GetRandomPointCloseToArea(Vector3 position, float maxRadius)
    {
        List<Transform> validPoints = new List<Transform>();
        foreach (var point in patrolPoints)
        {
            if (Vector3.Distance(position, point.position) < maxRadius)
            {
                validPoints.Add(point);
            }
        }
        if (validPoints.Count > 0)
        {
            return validPoints[(int)(Random.value * validPoints.Count)];
        }
        else
        {
            return null;
        }
    }

    public Transform GetRandomPoint()
    {
        return patrolPoints[(int)(Random.value * patrolPoints.Count)];
    }

    private void OnEnable()
    {
        current = this;
    }
}
