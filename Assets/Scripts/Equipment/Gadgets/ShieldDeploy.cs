using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeploy : MonoBehaviour
{
    public List<GameObject> sidePanels;

    private void Start()
    {
        foreach (var panel in sidePanels)
        {
            if (panel.activeInHierarchy)
            {
                panel.SetActive(false);
            }
        }
        StartCoroutine(DeployShield());
    }

    private IEnumerator DeployShield()
    {
        foreach (var panel in sidePanels)
        {
            panel.SetActive(true);
            yield return new WaitForSeconds(.05f);
        }
    }
}
