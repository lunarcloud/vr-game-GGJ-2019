using UnityEngine;

public class PlanetData
{
    public PlanetData(int seed)
    {
        var rand = new Generator(seed);

        // Generate location
        Location = new Vector3(
            rand.NextFloat() * 30f - 15f,
            rand.NextFloat() * 30f - 15f,
            rand.NextFloat() * 30f - 15f);

        // Generate star name
        StarName = NameGenerator.Create(rand.NextInt());

        // Select star type
        StarType = rand.NextFrom(StarType.Types);

        // Generate the star properties
        var starModifier = rand.NextFloat();
        StarTemperature = StarType.Temperature.Interpolate(starModifier);
        StarColor = StarType.BlackBodyColor(StarTemperature);
        StarMass = StarType.Mass.Interpolate(starModifier);
        StarHabitableZone = StarType.Habitable.Interpolate(starModifier);

        // Generate planet name
        PlanetName = NameGenerator.Create(rand.NextInt());

        // Generate planet distance
        PlanetDistance = StarHabitableZone * (0.9f + rand.NextFloat() * 0.2f);

        // Generate planet diameter
        PlanetDiameter = 7000 + rand.NextFloat() * 9000;

        // Calculate planet temperature
        PlanetTemperature = 14 + 200 * (StarHabitableZone / PlanetDistance - 1);

        // Pick gravity based on diameter
        PlanetGravity = Mathf.Pow(PlanetDiameter, 3) / 2e12f;
    }

    public Vector3 Location { get; }

    public string StarName { get; }

    public StarType StarType { get; }

    public float StarTemperature { get; }

    public Color StarColor { get; }

    public float StarMass { get; }

    public float StarHabitableZone { get; }
    
    public string PlanetName { get; }

    public float PlanetDistance { get; }

    public float PlanetDiameter { get; }

    public float PlanetTemperature { get; }

    public float PlanetGravity { get; }
}
