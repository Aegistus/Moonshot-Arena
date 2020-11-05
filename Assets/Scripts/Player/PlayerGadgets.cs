using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGadgets : MonoBehaviour
{
    private IGadget currentGadget;
    private List<IGadget> allGadgets = new List<IGadget>();

    private void Awake()
    {
        allGadgets.AddRange(GetComponentsInChildren<IGadget>());
        currentGadget = allGadgets[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentGadget.StartUse();
        }
        if (Input.GetMouseButtonUp(1))
        {
            currentGadget.EndUse();
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            GoToNextGadget();
        }
    }

    public void GoToNextGadget()
    {
        for (int i = 0; i < allGadgets.Count; i++)
        {
            if (allGadgets[i] == currentGadget)
            {
                if (i == allGadgets.Count - 1)
                {
                    currentGadget = allGadgets[0];
                    break;
                }
                else
                {
                    currentGadget = allGadgets[i + 1];
                    break;
                }
            }
        }
        print("Number of gadgets: " + allGadgets.Count);
    }

}
