using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barycenter : MonoBehaviour
{
    [SerializeField]
    protected OrbitalPath orbit;

    [SerializeField]
    protected Transform satelliteHolder;
    public List<Body> satellites = new List<Body>();
}
