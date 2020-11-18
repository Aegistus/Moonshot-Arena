using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public static ConstructionManager instance;

    public List<BlueprintEnemy> blueprints = new List<BlueprintEnemy>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
        GameObject[] bpObjects = GameObject.FindGameObjectsWithTag("Blueprint");
        foreach (var bp in bpObjects)
        {
            blueprints.Add(bp.GetComponent<BlueprintEnemy>());
        }
        BlueprintEnemy.OnFinishedBlueprint += RemoveFinishedBlueprint;
        BlueprintSpawner.OnNewBlueprintSpawned += AddNewBlueprint;
    }

    private void AddNewBlueprint(BlueprintEnemy obj)
    {
        blueprints.Add(obj);
    }

    private void RemoveFinishedBlueprint(BlueprintEnemy obj)
    {
        blueprints.Remove(obj);
    }

    public BlueprintEnemy GetNearestBlueprint(Vector3 position)
    {
        float closestDistance = Mathf.Infinity;
        BlueprintEnemy closest = null;
        foreach (BlueprintEnemy blueprint in blueprints)
        {
            float dist = Vector3.Distance(position, blueprint.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = blueprint;
            }
        }
        return closest;
    }
}
