using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrbitModel
{
    public Body primary;
    public float semiMajorAxis;
    public float eccentricity;

    public Vector3 GetPositionAt(float time)
    {
        return GetPositionAt(time, semiMajorAxis);
    }

    public Vector3 GetLogPositionAt(float time)
    {
        return GetPositionAt(time, Mathf.Log(semiMajorAxis));
    }

    Vector3 GetPositionAt(float time, float radius)
    {
        return new Vector3(
            Mathf.Cos(time) * radius,
            0,
            Mathf.Sin(time) * radius
        );
    }
}
