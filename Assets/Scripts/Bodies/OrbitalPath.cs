using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitalPath : MonoBehaviour
{
    public OrbitModel model;
    LineRenderer lineRenderer;
    public void Initialize(OrbitModel model)
    {
        this.model = model;
        lineRenderer = GetComponent<LineRenderer>();
        Redraw(32, ViewMode.Log);
    }

    public BarycenterModel GetPrimary()
    {
        return model.primary;
    }
    
    public void Redraw(int count, ViewMode mode)
    {
        if(model == null || model.primary == null) return;
        
        float seg = Mathf.PI * 2 * model.period / count;
        List<Vector3> positions = new List<Vector3>();
        for(int i = 0; i < count; i++)
        {
            positions.Add(model.GetRelativePositionAtTime(i*seg));
        }

        lineRenderer.positionCount = count;
        lineRenderer.SetPositions(positions.ToArray());
    }

    public Vector3 GetPostitionAt(float time, ViewMode mode)
    {
        return model.GetRelativePositionAtTime(time);
    }
}
