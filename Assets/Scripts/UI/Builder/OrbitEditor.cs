using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitEditor : MonoBehaviour
{
    public OrbitModel orbitModel;
    public BisectingLine semiMajorVisual;
    public BisectingLine ascendingVisual;
    public Transform semiMajorControls;
    public LineRenderer ellipse;

    void Awake()
    {
        orbitModel.eccentricity = orbitModel.eccentricity;
        UpdateVisuals();
    }

    public void AdjustPeriapsis(SlideToggle toggle)
    {
        AdjustPeriapsis(toggle.value);
    }
    
    public void AdjustPeriapsis(float magnitude)
    {
        orbitModel.periapsis += 0.01f * magnitude;
        UpdateVisuals();
    }

    public void SetPeriapsis(float periapsis)
    {
        orbitModel.periapsis = periapsis;
        UpdateVisuals();
    }

    public void AdjustSemiMajorAxis(SlideToggle toggle)
    {
        AdjustSemiMajorAxis(toggle.value);
    }
    
    public void AdjustSemiMajorAxis(float magnitude)
    {
        Debug.Log(1f + (0.1f * magnitude));
        orbitModel.semiMajorAxis *= 1f + (0.1f * magnitude);
        UpdateVisuals();
    }

    public void SetSemiMajorAxis(float semiMajorAxis)
    {
        orbitModel.semiMajorAxis = semiMajorAxis;
        UpdateVisuals();
    }

    public void AdjustMeanAnomaly(SlideToggle toggle)
    {
        AdjustMeanAnomaly(toggle.value);
    }

    public void AdjustMeanAnomaly(float magnitude)
    {
        orbitModel.meanAnomaly += 0.01f * magnitude;
        UpdateVisuals();
    }

    public void SetMeanAnomaly(float MeanAnomaly)
    {
        orbitModel.meanAnomaly = MeanAnomaly;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        float period = orbitModel.period;
        SetPoints(semiMajorVisual, orbitModel.periapsis + orbitModel.ascendingNode);
        semiMajorControls.rotation = Quaternion.Euler(0, -orbitModel.MeanToEccentricAnomaly(orbitModel.periapsis + orbitModel.ascendingNode) * Mathf.Rad2Deg + 90, 0);
        SetPoints(ascendingVisual, orbitModel.ascendingNode);
        SetEllipse();
    }

    void SetPoints(BisectingLine line, float angle)
    {
        line.SetPoints(
            orbitModel.GetRelativePositionAtAngle(angle), //Quaternion.Euler(90, 0, 0) * 
            new Vector3(0, 0, 0),
            orbitModel.GetRelativePositionAtAngle(angle + Mathf.PI));
    }

    void SetEllipse()
    {
        int count = Mathf.RoundToInt(Astrophysics.PI_2 * orbitModel.semiMajorAxis);
        Vector3[] positions = new Vector3[count];

        for(int i = 0; i < count ; i++)
        {
            positions[i] = orbitModel.GetRelativePositionAtAngle(Astrophysics.PI_2 * i / (float) count);
        }

        ellipse.positionCount = count;
        ellipse.SetPositions(positions);
    }
}
