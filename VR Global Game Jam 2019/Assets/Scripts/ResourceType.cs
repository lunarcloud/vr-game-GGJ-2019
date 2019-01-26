public class ResourceType : IProbability
{
    public static readonly ResourceType Water = new ResourceType(1f, "Water", 1000f, 1f);
    public static readonly ResourceType LiquidOxygen = new ResourceType(1f, "Liquid Oxygen", 1140f, 1f);
    public static readonly ResourceType Steel = new ResourceType(1f, "Steel", 7850f, 1f);
    public static readonly ResourceType Lithium = new ResourceType(1f, "Lithium", 534f, 1f);
    public static readonly ResourceType Uranium = new ResourceType(1f, "Uranium", 0.01905f, 0.000001f);

    public static readonly ResourceType[] Types =
    {
        Water,
        LiquidOxygen,
        Steel,
        Lithium,
        Uranium
    };

    private ResourceType(float prob, string name, float mass, float volume)
    {
        Probability = prob;
        Name = name;
        Mass = mass;
        Volume = volume;
    }

    public float Probability { get; }
    public string Name { get; }

    /// <summary>
    /// Mass of a unit of resource (1 = 1kg)
    /// </summary>
    public float Mass { get; }

    /// <summary>
    /// Volume of a unit of resource (1 = 1 cubic meter)
    /// </summary>
    public float Volume { get; }
};