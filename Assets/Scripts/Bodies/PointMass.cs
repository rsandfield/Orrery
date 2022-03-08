using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointMass
{
    [SerializeField]
    public BodyModel body { get; private set; }
    [SerializeField]
    public OrbitModel orbit { get; private set; }
    [SerializeField]
    public List<PointMass> satellites = new List<PointMass>();
    public string name { get => body == null ? "Binary" : body.name; }
    public float mass { get; protected set; }    public float hillRadius { get; protected set; }

    public PointMass(BodyModel body, OrbitModel orbit)
    {
        this.body = body;
        this.orbit = orbit;
        
        hillRadius = orbit.semiMajorAxis * Mathf.Pow(body.mass / (3 * orbit.primary.mass), 1/3.0f);
    }
}
