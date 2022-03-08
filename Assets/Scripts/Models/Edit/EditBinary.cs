public class EditBinary : EditBarycenter
{
    public EditBody a;
    public EditBody b;

    public override BarycenterModel ToModel(BarycenterModel primary = null)
    {
        BarycenterModel barry = new BarycenterModel(_name, orbit.ToModel());
        barry.SetSatellitesAndMass(new BarycenterModel[] {
            a.ToModel(barry),
            b.ToModel(barry)
        });
        return barry;
    }
}