using System.Collections.Generic;

public class AtmosphereType : IProbability
{
    public static readonly AtmosphereType Vacuum = new AtmosphereType
    {
        Probability = 0.1f,
        Name = "Vacuum",
        HabitableModifier = new RangeFloat(0.5f, 1.5f),
        ResourceModifiers = new Dictionary<ResourceType, RangeFloat>
        {
            { ResourceType.Water, new RangeFloat(10f, 50f) },
            { ResourceType.Lox, new RangeFloat(2f, 5f) }
        },
        AllowedTerrains = new List<TerrainType>
        {
            TerrainType.BarrenRock,
            TerrainType.IceWorld
        }
    };

    public static readonly AtmosphereType NitrogenOxygen = new AtmosphereType
    {
        Probability = 0.9f,
        Name= "Nitrogen/Oxygen",
        HabitableModifier = new RangeFloat(0.9f, 1.1f),
        ResourceModifiers = new Dictionary<ResourceType, RangeFloat>
        {
            { ResourceType.Water, new RangeFloat(0.1f, 0.8f) }
        },
        AllowedTerrains = new List<TerrainType>
        {
            TerrainType.IceWorld,
            TerrainType.EarthWorld
        }
    };

    public static readonly AtmosphereType Methane = new AtmosphereType
    {
        Probability = 0.1f,
        Name = "Methane",
        HabitableModifier = new RangeFloat(0.8f, 1.3f),
        AllowedTerrains = new List<TerrainType>
        {
            TerrainType.IceWorld,
            TerrainType.EarthWorld
        }
    };

    public static readonly AtmosphereType Ammonia = new AtmosphereType
    {
        Probability = 0.1f,
        Name = "Ammonia",
        HabitableModifier = new RangeFloat(0.7f, 1.1f),
        AllowedTerrains = new List<TerrainType>
        {
            TerrainType.IceWorld,
            TerrainType.EarthWorld
        }
    };

    public static readonly AtmosphereType Nitrogen = new AtmosphereType
    {
        Probability = 0.1f,
        Name = "Nitrogen",
        HabitableModifier = new RangeFloat(0.7f, 1.6f),
        AllowedTerrains = new List<TerrainType>
        {
            TerrainType.IceWorld,
            TerrainType.EarthWorld
        }
    };

    public static readonly AtmosphereType HydrogenSulfide = new AtmosphereType
    {
        Probability = 0.1f,
        Name = "Hydrogen Sulfide",
        HabitableModifier = new RangeFloat(0.8f, 1.3f),
        AllowedTerrains = new List<TerrainType>
        {
            TerrainType.IceWorld,
            TerrainType.EarthWorld
        }
    };

    public static readonly AtmosphereType[] Types =
    {
        Vacuum,
        NitrogenOxygen,
        Methane,
        Ammonia,
        Nitrogen,
        HydrogenSulfide
    };

    public float Probability { get; private set; }
    public string Name { get; private set; }
    public RangeFloat HabitableModifier { get; private set; }
    public Dictionary<ResourceType, RangeFloat> ResourceModifiers { get; private set; }
    public List<TerrainType> AllowedTerrains { get; private set; }
    public override string ToString() => Name;
}
