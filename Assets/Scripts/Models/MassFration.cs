[System.Serializable]
public struct MassFraction
{
    public float total;
    public float volitileFraction;
    public float lithicFraction;
    public float metallicFraction;
    public float volitileMass { get => total * volitileFraction; }
    public float lithicMass { get => total * lithicFraction; }
    public float metallicMass { get => total * metallicFraction; }

    public MassFraction(float total, float volitileFraction, float lithicFraction, float metallicFraction)
    {
        // Correct the fractions so that they sum to 1;
        float totalFraction = volitileFraction + lithicFraction + metallicFraction;
        if(totalFraction == 0) totalFraction = 1;

        this.total = total;
        this.volitileFraction = volitileFraction / totalFraction;
        this.lithicFraction = lithicFraction / totalFraction;
        this.metallicFraction = metallicFraction / totalFraction;
    }

    public MassFraction(float volitileMass, float lithicMass, float metallicMass)
    {
        total = volitileMass + lithicMass + metallicMass;
        volitileFraction = volitileMass / total;
        lithicFraction = lithicMass / total;
        metallicFraction = metallicMass / total;
    }
}