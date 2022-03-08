[System.Serializable]
public struct MassFraction
{
    public float mass;
    public float volitileFraction;
    public float lithicFraction;
    public float metallicFraction;
    public float volitileMass { get => mass * volitileFraction; }
    public float lithicMass { get => mass * lithicFraction; }
    public float metallicMass { get => mass * metallicFraction; }

    public MassFraction(float total, float volitileFraction, float lithicFraction, float metallicFraction)
    {
        // Correct the fractions so that they sum to 1;
        float totalFraction = volitileFraction + lithicFraction + metallicFraction;
        if(totalFraction == 0) totalFraction = 1;

        this.mass = total;
        this.volitileFraction = volitileFraction / totalFraction;
        this.lithicFraction = lithicFraction / totalFraction;
        this.metallicFraction = metallicFraction / totalFraction;
    }

    public MassFraction(float volitileMass, float lithicMass, float metallicMass)
    {
        mass = volitileMass + lithicMass + metallicMass;
        volitileFraction = volitileMass / mass;
        lithicFraction = lithicMass / mass;
        metallicFraction = metallicMass / mass;
    }
}