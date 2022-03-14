using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyModel : BarycenterModel
{
    public float radius;
    public MassFraction massFractions;

    public BodyModel(string name, MassFraction mass, OrbitModel orbit = null, BarycenterModel[] satellites = null) : base(name)
    {   
        this.orbit = orbit;
        this.massFractions = mass;
        this.mass = mass.total;
        this.radius = Mathf.Pow(mass.total, 1f/3f);
        this.satellites = satellites;
    }
}