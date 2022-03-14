using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField]
    GameObject bodyMesh;
    [SerializeField]
    Transform satelliteHolder;

    [SerializeField]
    OrbitalPath orbit;

    [SerializeField]
    GameObject hillSphereMesh;

    public BodyModel model { get; protected set; }
    public List<Body> satellites = new List<Body>();

    public static Body Instantiate(BodyModel model)
    {
        Body body = (GameObject.Instantiate(Resources.Load("Prefabs/Body")) as GameObject).GetComponent<Body>();

        body.name = model.name;
        body.model = model;

        body.InitializeOrbit();

        return body;
    }

    void InitializeOrbit()
    {
        if(model == null || model.orbit == null || model.orbit.primary == null)
        {
            orbit.gameObject.SetActive(false);
        }
        else
        {
            orbit.Initialize(model.orbit);
        }
    }

    void InitializeHillSphere(OrbitModel model)
    {
        if(model == null || model.primary == null)
        {
            hillSphereMesh.SetActive(false);
            return;
        }

        Mesh mesh = hillSphereMesh.GetComponent<MeshFilter>().mesh;
        // mesh.InvertNormals();
    }

    public void SetScaling(ViewMode mode)
    {
        orbit.Redraw(32, mode);

        if(model != null)
        {
            float pr = mode.scaleValue(model.radius);
            bodyMesh.transform.localScale = new Vector3(pr, pr, pr);
        }
        
        if(model.hillRadius > 0)
        {
            float hsr = mode.scaleValue(model.hillRadius) * 2;
            hillSphereMesh.transform.localScale = new Vector3(hsr, hsr, hsr);
        }
    }

    public void AddSatellite(Body body)
    {
        satellites.Add(body);
        body.transform.parent = satelliteHolder;
    }

    public Vector3 GetPositionAt(float time, ViewMode mode)
    {
        if(orbit.model.primary == null) return Vector3.zero;
        return orbit.GetPostitionAt(time, mode);
    }

    public void SetPosition(float time, ViewMode mode)
    {
        if(orbit.model.primary != null) 
        {
            Vector3 position = orbit.GetPostitionAt(time, mode);
            transform.localPosition = position;
            orbit.transform.localPosition = -position;
        }
        foreach(Body satellite in satellites)
        {
            satellite.SetPosition(time, mode);
        }
    }
}
