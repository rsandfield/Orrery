using System;
using UnityEngine;

public static class Vector3Helper
{
    public static bool HasNaN(this Vector3 vector)
    {
        return Single.IsNaN(vector.x) ||Single.IsNaN(vector.y) ||Single.IsNaN(vector.z);
    }
}