using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrbitModel
{
    public Body primary;
    public float semiMajorAxis { get; private set; }
    public float semiMinorRatio { get; private set; }
    public float eccentricity { get; private set; }
    public float linearEccentricity { get; private set; }
    public float inclination { get; private set; }
    public float periapsis { get; private set; }
    public float ascendingNode { get; private set; }
    Vector3 ascendingAxis;
    public float meanAnomaly { get; private set; }
    public float period { get; private set; }
    public float mu { get; private set; }
    EpochPositions positions = new EpochPositions(10);

    public OrbitModel(
        BarycenterModel primary, float semiMajorAxis, float eccentricity = 0f,
        float inclination = 0f, float ascendingNode = 0f, float periapsis = 0f,
        float meanAnomaly = 0f)
    {
        this.semiMajorAxis = semiMajorAxis;
        this.eccentricity = eccentricity;
        semiMinorRatio = Mathf.Sqrt(1 - Mathf.Pow(eccentricity, 2));
        linearEccentricity = Mathf.Sqrt(Mathf.Pow(semiMajorAxis, 2) - semiMajorAxis * Mathf.Pow(semiMinorRatio, 2));
        
        this.inclination = inclination;
        this.ascendingNode = ascendingNode;
        ascendingAxis = new Vector3(Mathf.Cos(ascendingNode), 0, Mathf.Sin(ascendingNode));
        this.periapsis = periapsis;
        this.meanAnomaly = meanAnomaly;

        mu = primary.mass * Astrophysics.G;;
        period = Mathf.Sqrt(Mathf.Pow(semiMajorAxis, 3) / mu) * Astrophysics.PI_2 * 1000;
    }

    public Vector3 GetRelativePositionAt(float epoch)
    {
        return GetRelativePositionAt(epoch, semiMajorAxis);
    }

    Vector3 GetRelativePositionAt(float epoch, float radius)
    {
        if(positions.Get(epoch) is Vector3 position)
        {
            return position;
        }
        else
        {
            float meanAnomaly = FloatHelper.ClampLooping(this.meanAnomaly + epoch / period, 0, 1) * Astrophysics.PI_2;
            float eccentricAnomaly = Astrophysics.ApproximateEccentricAnomaly(meanAnomaly, eccentricity);

            position = new Vector3(
                Mathf.Cos(eccentricAnomaly) * radius - linearEccentricity,
                0,
                Mathf.Sin(eccentricAnomaly) * radius * semiMinorRatio
            );
            position = Quaternion.AngleAxis(periapsis + ascendingNode, Vector3.up) * position;
            position = Quaternion.AngleAxis(inclination, ascendingAxis) * position;

            positions.Push(epoch, position);
            return position;
        }
    }
}
