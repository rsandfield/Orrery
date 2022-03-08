[System.Serializable]
public class EditOrbit
{
    public EditBody primary;
    public float semiMajorAxis;
    public float eccentricity;
    public float inclination;
    public float longitudeOfAscendingNode;
    public float argumentOfPeriapsis;
    public float startingMeanAnomaly;

    public OrbitModel ToModel(BarycenterModel primary = null)
    {
        return new OrbitModel(
            primary, semiMajorAxis, eccentricity,
            inclination, longitudeOfAscendingNode, argumentOfPeriapsis,
            startingMeanAnomaly
        );
    }
}