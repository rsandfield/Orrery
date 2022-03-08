[System.Serializable]
public class EditBody : EditBarycenter
{
    public MassFraction mass;

    public override BarycenterModel ToModel(BarycenterModel primary = null)
    {
        BodyModel body = new BodyModel(_name, mass.mass, orbit.ToModel(primary));
        body.SetSatellites(SatellitesToModels(body));
        return body;
    }
}