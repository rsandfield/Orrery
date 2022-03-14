using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleInput : MonoBehaviour
{
    [SerializeField]
    InputField input;
    [SerializeField]
    Dropdown dropdown;

    public void UnitChange()
    {
        switch(dropdown.value)
        {
            case 0:
                input.text = $"{float.Parse(input.text) * Mathf.Rad2Deg}";
                break;
            case 1:
                input.text = $"{float.Parse(input.text) * Mathf.Deg2Rad}";
                break;
        }
    }

    public float GetRadians()
    {
        if(dropdown.value == 0)
        {
            return float.Parse(input.text) * Mathf.Deg2Rad;
        }
        else
        {
            return float.Parse(input.text);
        }
    }

    public float GetDegrees()
    {
        if(dropdown.value == 0)
        {
            return float.Parse(input.text);
        }
        else
        {
            return float.Parse(input.text) * Mathf.Rad2Deg;
        }
    }
}
