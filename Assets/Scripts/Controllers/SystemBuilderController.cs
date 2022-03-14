using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBuilderController : MonoBehaviour
{
    [SerializeField]
    public BarycenterModel root;
    
    void Start()
    {
        BodyModel model = new BodyModel("Sol", new MassFraction(Astrophysics.SOLAR_MASS, 98.4f, 1.1f, 0.5f));
        root = model;

        LogRoot();
    }

    public void LogRoot()
    {
        Debug.Log(JsonUtility.ToJson(root));
    }
}
