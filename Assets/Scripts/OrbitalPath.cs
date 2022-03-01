using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitalPath : MonoBehaviour
{
    OrbitModel model;
    LineRenderer lineRenderer;
    public void Initialize(OrbitModel model)
    {
        this.model = model;
        lineRenderer = GetComponent<LineRenderer>();
        Redraw(32);
    }

    public Body GetPrimary()
    {
        return model.primary;
    }
    
    void Redraw(int count)
    {
        float seg = Mathf.PI * 2 / count;
        List<Vector3> positions = new List<Vector3>();
        for(int i = 0; i < count; i++)
        {
            positions.Add(model.GetPositionAt(i*seg));
        }

        lineRenderer.positionCount = count;
        lineRenderer.SetPositions(positions.ToArray());
    }

    public Vector3 GetPostitionAt(float time)
    {
        return model.GetPositionAt(time);
    }

    public Vector3 GetLogPositionAt(float time)
    {
        return model.GetLogPositionAt(time);
    }
}
