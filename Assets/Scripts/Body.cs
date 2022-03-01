using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField]
    OrbitalPath orbit;

    [SerializeField]
    GameObject model;

    [SerializeField]
    Transform satelliteHolder;
    List<Body> satellites = new List<Body>();

    public Body primary { get; protected set; }
    public float mass { get; protected set; }
    public float radius { get; protected set; }

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

        body.InitializeOrbit(orbitModel);
        body.ScalePlanet(Mathf.Pow(mass, 1f/3f));

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

        SetPosition(0);
    }

    void ScalePlanet(float scale)
    {
        model.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void AddSatellite(Body body)
    {
        satellites.Add(body);
        body.transform.parent = satelliteHolder;
    }

    public Vector3 GetPositionAt(float time)
    {
        if(primary == null) return Vector3.zero;
        return orbit.GetPostitionAt(time);
    }

    public Vector3 GetLogPositionAt(float time)
    {
        if(primary == null) return Vector3.zero;
        return orbit.GetLogPositionAt(time);   
    }

    public void SetPosition(float time)
    {
        if(primary == null) return;

        Vector3 position = GetPositionAt(time);
        transform.localPosition = position;
        orbit.transform.localPosition = -position;
    }
}
