using System;
using System.Collections.Generic;
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
        StarName = AlienNameGenerator.Create(rand.NextInt());

        // Select star type
        StarType = rand.NextFrom(StarType.Types);

        // Generate the star properties
        var starModifier = rand.NextFloat();
        StarTemperature = StarType.Temperature.Interpolate(starModifier);
        StarColor = StarType.BlackBodyColor(StarTemperature);
        StarMass = StarType.Mass.Interpolate(starModifier);
        StarHabitableZone = StarType.Habitable.Interpolate(starModifier);

        // Generate planet name
        PlanetName = AlienNameGenerator.Create(rand.NextInt());

        // Select planet composition
        PlanetComposition = rand.NextFrom(PlanetCompositionType.Types);

        // Select atmosphere
        PlanetAtmosphere = rand.NextFrom(AtmosphereType.Types);

        // Generate planet distance
        PlanetDistance = StarHabitableZone * PlanetAtmosphere.HabitableModifier.Interpolate(rand.NextFloat());

        // Generate planet diameter
        PlanetDiameter = 7000 + rand.NextFloat() * 9000;

        // Calculate planet temperature
        PlanetTemperature = 14 + 200 * (StarHabitableZone / PlanetDistance - 1);

        // Pick gravity based on diameter
        PlanetGravity = Mathf.Pow(PlanetDiameter, 3) / 2e12f;

        // Populate the resource costs
        ResourceCosts = new Dictionary<ResourceType, long>();
        foreach (var res in ResourceType.Types)
        {
            // Get base cost
            var cost = res.Cost.Interpolate(rand.NextFloat());

            // Apply modifier due to planet composition
            if (PlanetComposition.ResourceModifiers?.ContainsKey(res) ?? false)
                cost *= PlanetComposition.ResourceModifiers[res].Interpolate(rand.NextFloat());

            // Apply modifier due to atmospheric composition
            if (PlanetAtmosphere.ResourceModifiers?.ContainsKey(res) ?? false)
                cost *= PlanetAtmosphere.ResourceModifiers[res].Interpolate(rand.NextFloat());

            ResourceCosts[res] = (long)Math.Round(cost);
        }
    }

    public Vector3 Location { get; }

    public string StarName { get; }

    public StarType StarType { get; }

    public float StarTemperature { get; }

    public Color StarColor { get; }

    public float StarMass { get; }

    public float StarHabitableZone { get; }
    
    public string PlanetName { get; }

    public PlanetCompositionType PlanetComposition { get; }

    public AtmosphereType PlanetAtmosphere { get; }

    public float PlanetDistance { get; }

    public float PlanetDiameter { get; }

    public float PlanetTemperature { get; }

    public float PlanetGravity { get; }

    public Dictionary<ResourceType, long> ResourceCosts { get; }
}
