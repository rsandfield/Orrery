using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyModel : BarycenterModel
{
    public float radius { get; protected set; }

    public BodyModel(string name, float mass, OrbitModel orbit = null, BarycenterModel[] satellites = null) : base(name)
    {   
        this.orbit = orbit;
        this.mass = mass;
        this.radius = Mathf.Pow(mass, 1f/3f);
        this.satellites = satellites;
    }
}