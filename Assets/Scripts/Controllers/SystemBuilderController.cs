using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBuilderController : MonoBehaviour
{
    [SerializeField]
    public EditBarycenter root = new EditBody();
    

    void Start()
    {
        EditBody body = new EditBody();
        body.mass = new MassFraction(Astrophysics.SOLAR_MASS, 98.4f, 1.1f, 0.5f);
        root = body;

        LogRoot();
    }

    public void LogRoot()
    {
        Debug.Log(JsonUtility.ToJson(root));
    }
}
