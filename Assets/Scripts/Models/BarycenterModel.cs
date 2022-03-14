[System.Serializable]
public class BarycenterModel
{
    public string name;
    public float mass;
    public float hillRadius;
    public OrbitModel orbit;
    public BarycenterModel[] satellites;

    protected BarycenterModel(string name)
    {
        this.name = name;
    }

    public BarycenterModel(string name, OrbitModel orbit = null, BarycenterModel[] satellites = null)
    {
        this.name = name;
        this.orbit = orbit;
        this.satellites = satellites;
    }

    float GetMassFromSatellites()
    {
        float mass = 0;
        
        if(satellites != null && satellites.IsNotEmpty())
        {
            foreach(BarycenterModel satellite in satellites)
            {
                mass += satellite.mass;
            }
        }
        return mass;
    }

    public void SetSatellites(BarycenterModel[] satellites)
    {
        this.satellites = satellites;
    }

    public void SetSatellitesAndMass(BarycenterModel[] satellites)
    {
        this.satellites = satellites;
        mass = GetMassFromSatellites();
    }
}