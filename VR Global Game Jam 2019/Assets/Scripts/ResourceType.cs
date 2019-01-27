public class ResourceType : IProbability
{
    public static readonly ResourceType Water = new ResourceType
    {
        Probability = 1f,
        Name = "Water",
        Volume = 1f,                        // Unit = 1 Meter^3
        Mass = 1000f,                       // Unit = 1000 KG
        Cost = new RangeFloat(80f, 120f)    // Unit = 80-120
    };

    public static readonly ResourceType LiquidOxygen = new ResourceType
    {
        Probability = 1f,
        Name = "Liquid Oxygen",
        Volume = 1f,                        // Unit = 1 Meter^3
        Mass = 1140f,                       // Unit = 1140 KG
        Cost = new RangeFloat(300f, 500f)   // Unit = 300-500
    };

    public static readonly ResourceType Steel = new ResourceType
    {
        Probability = 1f,
        Name = "Steel",
        Volume = 1f,                        // Unit = 1 Meter^3
        Mass = 7850f,                       // Unit = 7850 KG
        Cost = new RangeFloat(1200f, 1600f) // Unit = 1200-1500
    };

    public static readonly ResourceType Lithium = new ResourceType
    {
        Probability = 0.5f,
        Name = "Lithium",
        Volume = 1f,                        // Unit = 1 Meter^3
        Mass = 534f,                        // Unit = 534 KG
        Cost = new RangeFloat(2500f, 3300f) // Unit = 2500-3300
    };

    public static readonly ResourceType Uranium = new ResourceType
    {
        Probability = 0.1f,
        Name = "Uranium",
        Volume = 0.000001f,                 // Unit = 1 Cm^3
        Mass = 0.01905f,                    // Unit = 19.05 G
        Cost = new RangeFloat(1500f, 2500f) // Unit = 1500-2500
    };

    public static readonly ResourceType[] Types =
    {
        Water,
        LiquidOxygen,
        Steel,
        Lithium,
        Uranium
    };

    public float Probability { get; private set; }

    public string Name { get; private set; }

    /// <summary>
    /// Volume of a unit of resource (1 = 1 cubic meter)
    /// </summary>
    public float Volume { get; private set; }

    /// <summary>
    /// Mass of a unit of resource (1 = 1kg)
    /// </summary>
    public float Mass { get; private set; }

    public RangeFloat Cost { get; private set; }

    public override string ToString() => Name;
};