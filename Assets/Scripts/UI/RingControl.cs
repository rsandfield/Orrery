using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class RingControl : CircleRenderer
{
    new public EdgeCollider2D collider;
    public override float radius
    {
        get => base.radius;
        set
        {
            base.radius = value;
            List<Vector2> points = new List<Vector2>();
            Vector3[] positions = new Vector3[line.positionCount];
            int count = line.GetPositions(positions);
            foreach(Vector3 pos in positions)
            {
                points.Add(pos);
            }
            collider.SetPoints(points);
        }
    }
    public override float lineWidth
    {
        get => base.lineWidth;
        set
        {
            base.lineWidth = value;
            collider.edgeRadius = value;
        }
    }

    protected override void Reset()
    {
        base.Reset();
        collider = GetComponent<EdgeCollider2D>();
    }
}
