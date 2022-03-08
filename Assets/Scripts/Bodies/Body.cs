using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Barycenter
{
    [SerializeField]
    GameObject bodyMesh;

    [SerializeField]
    GameObject hillSphereMesh;

    public Body primary { get; protected set; }
    [field: SerializeField]
    public float mass { get; protected set; }
    [field: SerializeField]
    public float radius { get; protected set; }
    [field: SerializeField]
    public float hillRadius { get; protected set; }

    public static Body Instantiate(BodyModel model)
    {
        return Instantiate(model.name, model.mass, model.orbit);
    }

    public static Body Instantiate(string name, float mass, OrbitModel orbitModel = null)
    {
        Body body = (GameObject.Instantiate(Resources.Load("Prefabs/Body")) as GameObject).GetComponent<Body>();

        body.gameObject.name = name;
        body.name = name;
        body.mass = mass;
        body.radius = Mathf.Pow(mass, 1f/3f);

        body.InitializeOrbit(orbitModel);
        body.InitializeHillSphere(orbitModel);

        return body;
    }

    void InitializeOrbit(OrbitModel orbitModel)
    {
        if(orbitModel == null || orbitModel.primary == null)
        {
            orbit.gameObject.SetActive(false);
            primary = null;
        }
        else
        {
            orbit.Initialize(orbitModel);
            primary = orbit.GetPrimary();
            primary.AddSatellite(this);
        }
    }

    void InitializeHillSphere(OrbitModel model)
    {
        if(model == null || model.primary == null)
        {
            hillSphereMesh.SetActive(false);
            return;
        }

        hillRadius = model.semiMajorAxis * Mathf.Pow(mass / (3 * model.primary.mass), 1/3.0f);

        Mesh mesh = hillSphereMesh.GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;
        for(int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        int[] tris = mesh.GetTriangles(0);
        for(int i = 0; i < tris.Length; i += 3)
        {
            int temp = tris[i];
            tris[i] = tris[i + 1];
            tris[i + 1] = temp;
        }
        mesh.SetTriangles(tris, 0);
    }

    public void SetScaling(ViewMode mode)
    {
        orbit.Redraw(32, mode);
        float planetaryRadius = mode.scaleValue(radius);
        bodyMesh.transform.localScale = new Vector3(planetaryRadius, planetaryRadius, planetaryRadius);
        
        if(hillRadius == 0) return;
        float hillSphereRadius = mode.scaleValue(hillRadius) * 2;
        hillSphereMesh.transform.localScale = new Vector3(hillSphereRadius, hillSphereRadius, hillSphereRadius);
    }

    public void AddSatellite(Body body)
    {
        satellites.Add(body);
        body.transform.parent = satelliteHolder;
    }

    public Vector3 GetPositionAt(float time, ViewMode mode)
    {
        if(primary == null) return Vector3.zero;
        return orbit.GetPostitionAt(time, mode);
    }

    public void SetPosition(float time, ViewMode mode)
    {
        if(primary != null) 
        {
            Vector3 position = GetPositionAt(time, mode);
            transform.localPosition = position;
            orbit.transform.localPosition = -position;
        }
        foreach(Body satellite in satellites)
        {
            satellite.SetPosition(time, mode);
        }
    }
}
