using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EditBarycenter : MonoBehaviour
{
    public string _name;
    [SerializeField]
    public EditOrbit orbit;
    [SerializeField]
    public List<EditBarycenter> satellites;

    public abstract BarycenterModel ToModel(BarycenterModel primary = null);

    public BarycenterModel[] SatellitesToModels(BarycenterModel body)
    {
        BarycenterModel[] satelliteModels = new BarycenterModel[satellites.Count];
        for(int i = 0; i < satellites.Count; i++)
        {
            satelliteModels[i] = satellites[i].ToModel(body);
        }
        return satelliteModels;
    }
}