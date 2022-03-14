using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrbitModel
{
    [SerializeField]
    public BarycenterModel primary;
    [SerializeField]
    float _semiMajorAxis;
    public float semiMajorAxis
    {
        get => _semiMajorAxis;
        set
        {
            _semiMajorAxis = value > 1 ? value : 1;
            RecalculateDirivedElements();
        }
    }
    public float semiMinorRatio { get; private set; }
    [SerializeField]
    float _eccentricity;
    public float eccentricity {
        get => _eccentricity;
        set
        {
            _eccentricity = Mathf.Clamp01(value);
            RecalculateDirivedElements();
        }
    }
    // [ReadOnly]
    public float linearEccentricity { get; private set; }
    [SerializeField]
    float _ascendingNode;
    public float ascendingNode
    {
        get => _ascendingNode;
        set
        {
            _ascendingNode = FloatHelper.ClampLooping(value, 0, Astrophysics.PI_2);
            ascendingAxis = new Vector3(Mathf.Cos(ascendingNode), 0, Mathf.Sin(ascendingNode));
        }
    }
    public float inclination;
    [SerializeField]
    float _periapsis;
    public float periapsis
    {
        get => _periapsis;
        set
        {
            _periapsis = FloatHelper.ClampLooping(value, 0, Astrophysics.PI_2);
        }
    }
    Vector3 ascendingAxis { get; set; }
    [SerializeField]
    float _meanAnomaly;
    public float meanAnomaly
    {
        get => _meanAnomaly;
        set
        {
            _meanAnomaly = FloatHelper.ClampLooping(value, 0, Astrophysics.PI_2);
        }
    }
    // [ReadOnly]
    public float period;
    // [ReadOnly]
    public float mu;
    EpochPositions positions = new EpochPositions(10);

    public OrbitModel(
        BarycenterModel primary, float semiMajorAxis, float eccentricity = 0f,
        float inclination = 0f, float ascendingNode = 0f, float periapsis = 0f,
        float meanAnomaly = 0f)
    {
        this.primary = primary;

        _semiMajorAxis = semiMajorAxis;
        _eccentricity = eccentricity;
        
        this.inclination = inclination;
        this.ascendingNode = ascendingNode;
        this.periapsis = periapsis;
        this.meanAnomaly = meanAnomaly;

        RecalculateDirivedElements();
    }

    void RecalculateDirivedElements()
    {
        semiMinorRatio = Mathf.Sqrt(1 - Mathf.Pow(eccentricity, 2));
        linearEccentricity = Mathf.Sqrt(1 - Mathf.Pow(semiMinorRatio, 2));
        Debug.Log(linearEccentricity);
        mu = primary.mass * Astrophysics.G;;
        period = Mathf.Sqrt(Mathf.Pow(semiMajorAxis, 3) / mu) * Astrophysics.PI_2 * 1000;
    }

    public float GetMeanAnomalyAt(float epoch)
    {
        return FloatHelper.ClampLooping(this.meanAnomaly + epoch / period, 0, 1) * Astrophysics.PI_2;
    }

    public float MeanToEccentricAnomaly(float meanAnomaly)
    {
        return FloatHelper.ClampLooping(Astrophysics.ApproximateEccentricAnomaly(meanAnomaly, eccentricity), 0, Astrophysics.PI_2);
    }

    public Vector3 GetRelativePositionAtTime(float epoch)
    {
        if(positions != null && positions.Get(epoch) is Vector3 position)
        {
            return position;
        }
        else
        {
            position = GetRelativePositionAtAngle(GetMeanAnomalyAt(epoch));

            positions?.Push(epoch, position);
            return position;
        }
    }

    public Vector3 GetRelativePositionAtMeanAnomaly(float meanAnomaly)
    {
        return GetRelativePositionAtAngle(MeanToEccentricAnomaly(meanAnomaly));
    }

    public Vector3 GetRelativePositionAtAngle(float angle)
    {
        angle = FloatHelper.ClampLooping(angle, 0, Astrophysics.PI_2);

        float a = semiMajorAxis;
        float b = semiMajorAxis * semiMinorRatio;

        float tan = Mathf.Tan(angle);
        float denominator = Mathf.Sqrt(b * b + a * a * tan * tan);
        float x = a * b / denominator;
        if(angle >= Mathf.PI * 0.5f && angle < Mathf.PI * 1.5f) x *= -1;
        float y = x * tan;
        
        Vector3 position = new Vector3(
            x - linearEccentricity,
            0,
            y
        );
        // position = Quaternion.AngleAxis(periapsis + ascendingNode, Vector3.up) * position;
        // position = Quaternion.AngleAxis(inclination, ascendingAxis) * position;
        return position;
    }
}
