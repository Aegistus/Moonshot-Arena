using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGadget
{
    void EnableGadget();
    void DisableGadget();
    void StartUse();
    void EndUse();
}
