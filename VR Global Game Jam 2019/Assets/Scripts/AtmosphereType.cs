public class AtmosphereType : IProbability
{
    public static readonly AtmosphereType Vacuum = new AtmosphereType(0.1f, "Vacuum", 0.5f, 1.5f);
    public static readonly AtmosphereType NitrogenOxygen = new AtmosphereType(0.9f, "Nitrogen/Oxygen", 0.9f, 1.1f);
    public static readonly AtmosphereType Methane = new AtmosphereType(0.1f, "Methane", 0.8f, 1.3f);
    public static readonly AtmosphereType Ammonia = new AtmosphereType(0.1f, "Ammonia", 0.7f, 1.1f);
    public static readonly AtmosphereType Nitrogen = new AtmosphereType(0.1f, "Nitrogen", 0.7f, 1.6f);
    public static readonly AtmosphereType HydrogenSulfide = new AtmosphereType(0.1f, "HydrogenSulfide", 0.8f, 1.3f);

    public static readonly AtmosphereType[] Types =
    {
        Vacuum,
        NitrogenOxygen,
        Methane,
        Ammonia,
        Nitrogen,
        HydrogenSulfide
    };

    private AtmosphereType(float probability, string name, float minHab, float maxHab)
    {
        Probability = probability;
        Name = name;
        HabitableModifier = new RangeFloat(minHab, maxHab);
    }

    public float Probability { get; }
    public string Name { get; }
    public RangeFloat HabitableModifier { get; }
}
