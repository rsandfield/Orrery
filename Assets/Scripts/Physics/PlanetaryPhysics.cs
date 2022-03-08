using UnityEngine;

public static class PlanetaryPhysics
{
    /// <summary>
    /// Calculates the radius of a celestial body based on the total mass and
    /// mass fractions of the major elemental components
    /// </summary>
    /// <param name="volitiles">Water and other high-volitility material</param>
    /// <param name="lithics">Silicon and other lithophilic material</param>
    /// <param name="metals">Iron and other metallic material</param>
    /// <returns>Radius of body in meters</returns>
    public static float RadiusFromPartialMasses (float volitiles, float lithics, float metals) {
        float total = volitiles + lithics + metals;
        return Astrophysics.EARTH_RADIUS * RadiusFromMassAndFractions(
            total,
            volitiles / total,
            lithics / total,
            metals / total
        );
    }

    /// <summary>
    /// Calculates the approximate radius of a sub-stellar astronomical body
    /// from the total mass in kilograms and mass fractions of three classes
    /// of material
    /// </summary>
    /// <param name="total">Total mass in kilograms</param>
    /// <param name="volitiles">Water and other high-volitility material</param>
    /// <param name="lithics">Silicon and other lithophilic material</param>
    /// <param name="metals">Iron and other metallic material</param>
    /// <returns>Radius of body in meters</returns>
    public static float RadiusFromMassAndFractions(float total, float volitiles, float lithics, float metals) {
        // Make sure ratio total is not greater than 1
        float ratioSum = volitiles + lithics + metals;
        volitiles /= ratioSum;
        lithics /= ratioSum;
        metals /= ratioSum;

        float density = (1 / (volitiles + (lithics / 3.5f) + (metals / 7.8f)));

        if(total < 0.01 * Astrophysics.EARTH_MASS) {
            return RadiusUncompressFromMassAndDensity(total, density * 1000);
        }

        if(density < 3.5) {
            return RadiusCompressedWaterRockPlanet(total, (1.4f/density) - 0.4f);
        } else {
            return RadiusCompressedRockMetalPlanet(total, (6.369f/density) - 0.8140f);
        }
    }

    /**
     * 
     * @param mass Mass in kilograms
     * @param density Average density of material in kg/m3
     * @returns Radius of body in earth radii
     */
     /// <summary>
     /// Calculates the approximate radius of a sub-stellar astronomcial body
     /// from the mass in earth masses, assuming that gravitational compression
     /// does not come into play
     /// </summary>
     /// <param name="mass"></param>
     /// <param name="density"></param>
     /// <returns></returns>
    static float RadiusUncompressFromMassAndDensity(float mass, float density) {
        float volume = mass / density;
        float radius = Mathf.Pow(volume * 0.75f / Mathf.PI, 1/3f);
        return radius;
    }

    static float RadiusCompressedWaterRockPlanet(float mass, float volitileFraction) {
        mass /= Astrophysics.EARTH_MASS;
        return Astrophysics.EARTH_RADIUS * (
            (0.0912f * volitileFraction + 0.1603f) * Mathf.Pow(Mathf.Log10(mass), 2) +
            (0.3330f * volitileFraction + 0.7387f) * Mathf.Log10(mass) +
            0.4639f * volitileFraction + 1.1193f
        );
    }

    static float RadiusCompressedRockMetalPlanet(float mass, float lithicFraction) {
        mass /= Astrophysics.EARTH_MASS;
        return Astrophysics.EARTH_RADIUS * (
            (0.0592f * lithicFraction + 0.0975f) * Mathf.Pow(Mathf.Log10(mass), 2) +
            (0.2337f * lithicFraction + 0.4938f) * Mathf.Log10(mass) +
            0.3102f * lithicFraction + 0.7932f
        );
    }
}