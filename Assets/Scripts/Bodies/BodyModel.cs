using System.Collections.Generic;

[System.Serializable]
public class BodyModel
{
    public string name;
    public float mass;
    public OrbitModel orbit;
    public BodyModel[] satellites;
}