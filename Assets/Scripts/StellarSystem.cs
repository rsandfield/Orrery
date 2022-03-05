using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarSystem : MonoBehaviour
{
    public TextAsset system;
    public ViewMode mode = ViewMode.Log;
    Body root;
    public float time;
    public float speed = 1000;
    
    void Start()
    {
        BodyModel rootModel = JsonUtility.FromJson<BodyModel>(system.text);
        root = Body.Instantiate(rootModel);
        InitializeSubsystem(root, rootModel);
    }

    void InitializeSubsystem(Body primary, BodyModel primaryModel)
    {
        foreach(BodyModel satellite in primaryModel.satellites)
        {
            satellite.orbit.primary = primary;
            Body body = Body.Instantiate(satellite);
            InitializeSubsystem(body, satellite);
        }
    }

    public void ChangeViewMode(int mode)
    {
        this.mode = (ViewMode) mode;
        ScaleSubsystem(root);
    }

    void ScaleSubsystem(Body primary)
    {
        primary.SetScaling(mode);
        foreach(Body satellite in primary.satellites)
        {
            ScaleSubsystem(satellite);
        }
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        root.SetPosition(time, mode);
    }
}
