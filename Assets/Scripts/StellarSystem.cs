using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarSystem : MonoBehaviour
{
    public TextAsset system;
    
    void Start()
    {
        BodyModel root = JsonUtility.FromJson<BodyModel>(system.text);
        Body sun = Body.Instantiate(root);
        InitializeSubsystem(sun, root);
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
}
