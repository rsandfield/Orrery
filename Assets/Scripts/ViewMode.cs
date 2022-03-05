using UnityEngine;

public enum ViewMode
{
    Linear, Log, Magnitude
}

public static class ViewModeHelper
{
    public static float scaleValue(this ViewMode mode, float value)
    {
        switch(mode)
        {
            case ViewMode.Log:
                return Mathf.Pow(Mathf.Log(value), 0.5f);
            case ViewMode.Magnitude:
                return Mathf.Pow(value, 0.5f) / 1000;
            case ViewMode.Linear:
            default:
                return value / Mathf.Pow(10, 8);
        }
    }
}