using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitalPath : MonoBehaviour
{
    [SerializeField]
    OrbitModel model;
    LineRenderer lineRenderer;
    public void Initialize(OrbitModel model)
    {
        this.model = model;
        lineRenderer = GetComponent<LineRenderer>();
        Redraw(32, ViewMode.Log);
    }

    public Body GetPrimary()
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
<<<<<<<< HEAD:Assets/Scripts/Worldspace/OrbitalPath.cs
            positions.Add(model.GetRelativePositionAt(i*seg));
========
            positions.Add(model.GetPositionAt(i*seg, mode));
>>>>>>>> f5e686a005f683c51fa8766c32a632f5a15e0c83:Assets/Scripts/Bodies/OrbitalPath.cs
        }

        lineRenderer.positionCount = count;
        lineRenderer.SetPositions(positions.ToArray());
    }

    public Vector3 GetPostitionAt(float time, ViewMode mode)
    {
<<<<<<<< HEAD:Assets/Scripts/Worldspace/OrbitalPath.cs
        return model.GetRelativePositionAt(time);
========
        return model.GetPositionAt(time, mode);
>>>>>>>> f5e686a005f683c51fa8766c32a632f5a15e0c83:Assets/Scripts/Bodies/OrbitalPath.cs
    }
}
