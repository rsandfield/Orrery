using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BisectingLine : MonoBehaviour
{
    public LineRenderer solid;
    public LineRenderer dotted;
    [SerializeField]
    float _lineWidth = 1;
    public float lineWidth {
        get => _lineWidth;
        set
        {
            _lineWidth = value;
            solid.startWidth = value;
            float length = Vector3.Distance(dotted.GetPosition(0), dotted.GetPosition(1));
            int count = Mathf.RoundToInt(0.75f * length / value);
            dotted.startWidth = length / (float) count;
            solid.startWidth = value;
            dotted.material.mainTextureScale = new Vector2(count / length, 1.0f);
        }
    }

    void Reset()
    {
        solid = transform.Find("Solid").GetComponent<LineRenderer>();
        dotted = transform.Find("Dotted").GetComponent<LineRenderer>();
    }

    void Awake()
    {
        lineWidth = _lineWidth;
    }

    public void SetPoints(Vector3 solid, Vector3 center, Vector3 dotted)
    {
        if(solid == null || center == null || dotted == null) return;
        if(solid.HasNaN() || center.HasNaN() || dotted.HasNaN()) return;
        this.solid.SetPosition(1, solid);
        this.solid.SetPosition(0, center);
        this.dotted.SetPosition(0, center);
        this.dotted.SetPosition(1, dotted);
    }
}
