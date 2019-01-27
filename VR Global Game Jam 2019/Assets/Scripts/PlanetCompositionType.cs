using System.Collections.Generic;

public class PlanetCompositionType : IProbability
{
    public static readonly PlanetCompositionType NickelIron = new PlanetCompositionType
    {
        Probability = 0.6f,
        Name = "Nickel Iron",
        ResourceModifiers = new Dictionary<ResourceType, RangeFloat>
        {
            { ResourceType.Steel, new RangeFloat(0.5f, 0.8f) },
            { ResourceType.Lithium, new RangeFloat(0.5f, 0.8f) },
            { ResourceType.Uranium, new RangeFloat(0.5f, 0.8f) }
        }
    };

    public static readonly PlanetCompositionType Silicate = new PlanetCompositionType
    {
        Probability = 0.3f,
        Name = "Silicate",
        ResourceModifiers = new Dictionary<ResourceType, RangeFloat>
        {
            { ResourceType.Steel, new RangeFloat(1.2f, 2f) },
            { ResourceType.Lithium, new RangeFloat(1.2f, 2f) },
            { ResourceType.Uranium, new RangeFloat(1.2f, 2f) }
        }
    };

    public static readonly PlanetCompositionType[] Types =
    {
        NickelIron,
        Silicate
    };

    public float Probability { get; private set; }
    public string Name { get; private set; }
    public Dictionary<ResourceType, RangeFloat> ResourceModifiers { get; private set; }
    public override string ToString() => Name;
}