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
        StarMass = StarType.Mass.Interpolate(starModifier);
        StarHabitableZone = StarType.Habitable.Interpolate(starModifier);

        // Generate planet name
        PlanetName = NameGenerator.Create(rand.NextInt());
    }

    public Vector3 Location { get; }

    public string StarName { get; }

    public StarType StarType { get; }

    public float StarTemperature { get; }

    public float StarMass { get; }

    public float StarHabitableZone { get; }
    
    public string PlanetName { get; }
}