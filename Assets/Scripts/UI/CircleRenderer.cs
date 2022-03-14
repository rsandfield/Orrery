using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    [SerializeField]
    float _radius = 1f;
    public virtual float radius
    {
        get => _radius;
        set
        {
            _radius = value;
            Vector3[] positions = CalculateCircle(value);
            line.positionCount = positions.Length;
            line.SetPositions(positions);

        }
    }
    
    [SerializeField]
    float _lineWidth = 0.1f;
    public virtual float lineWidth
    {
        get => _lineWidth;
        set
        {
            _lineWidth = value;
            line.startWidth = value;
        }
    }
    public LineRenderer line;

    protected virtual void Reset()
    {
        line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        lineWidth = lineWidth;
        radius = radius;
    }

    void OnValidate()
    {
        lineWidth = lineWidth;
        radius = radius;
    }

    protected Vector3[] CalculateCircle(float radius)
    {
        int count = Mathf.RoundToInt(10 * Mathf.PI * radius);
        if(count < 16) count = 16;

        Vector3[] positions = new Vector3[count];
        for(int i = 0; i < count; i++)
        {
            float rad = i * Mathf.PI * 2 / (float) count;
            positions[i] = new Vector3(
                Mathf.Cos(rad) * radius,
                Mathf.Sin(rad) * radius
            );
        }
        return positions;
    }
}
