using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyModel
{
    public string name;
    public float mass;
    public float radius { get; protected set; }

    public BodyModel(string name, float mass)
    {
        this.name = name;
        this.mass = mass;
        
        radius = Mathf.Pow(mass, 1f/3f);
    }
}