using UnityEngine;

public static class Astrophysics
{
    public const float PI_2 = Mathf.PI * 2;
    public const float G = 6.6748e-11f;

    public const float STEFAN_BOLTZMAN = 5.670373e-8f;
    public const float SOLAR_LUMINOSITY = 3.939e26f;

    public const float EARTH_MASS = 5.972e24f;
    public const float JUPITER_MASS = 1.898e27f;
    public const float SOLAR_MASS = 1.98855e30f;

    public const float EARTH_RADIUS = 6.371e6f;
    public const float LUNAR_DISTANCE = 3.844e8f;
    public const float SOLAR_RADIUS = 6.9634e9f;
    public const float ASTRONOMICAL_UNIT = 1.674e11f;
    public const float LIGHT_YEAR = 9.4607e15f;
    public const float PARSEC = 3.0857e16f;

    static float NewtonsApproximation(float meanAnomaly, float eccentricity, float? prior)
    {
        if(prior is float value)
        {
            return value -
                (value - eccentricity * Mathf.Sin(value) - meanAnomaly) /
                (1f - eccentricity * Mathf.Cos(value));
        }
        else
        {
            return meanAnomaly;
        }
    }

    public static float ApproximateEccentricAnomaly(float meanAnomaly, float eccentricity, int maxIterations = 20)
    {
        float last = meanAnomaly;
        for(int i = 0; i < maxIterations; i++)
        {
            float next = NewtonsApproximation(meanAnomaly, eccentricity, last);
            if(next == last) break;
            last = next;
        }
        return last;
    }
}