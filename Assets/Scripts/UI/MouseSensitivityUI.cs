using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityUI : MonoBehaviour
{
    public static float mouseSensitivity = 200f;

    public void ChangeMouseSensitivity(InputField input)
    {
        mouseSensitivity = int.Parse(input.text);
    }

}
