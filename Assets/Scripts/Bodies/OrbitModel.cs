using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrbitModel
{
    public Body primary;
    public float semiMajorAxis;
    public float eccentricity;
    public float period { get => Mathf.Pow(semiMajorAxis, 0.5f); }

    public Vector3 GetPositionAt(float time, ViewMode mode)
    {
        return GetPositionAt(time, mode.scaleValue(semiMajorAxis));
    }

    Vector3 GetPositionAt(float time, float radius)
    {
        return new Vector3(
            Mathf.Cos(time / period) * radius,
            0,
            Mathf.Sin(time / period) * radius
        );
    }
}
