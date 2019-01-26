public class StarType : IProbability
{
    public static readonly StarType AType = new StarType(0.6f, "A", 7500f, 10000f, 1.4f, 2.1f, 3f, 9f);
    public static readonly StarType FType = new StarType(3f, "F", 6000f, 7500f, 1.04f, 1.4f, 1.8f, 2f);
    public static readonly StarType GType = new StarType(7.6f, "G", 5200f, 6000f, 0.8f, 1.04f, 0.7f, 1.8f);
    public static readonly StarType KType = new StarType(12.1f, "K", 3700f, 5200f, 0.45f, 0.8f, 0.35f, 0.7f);

    public static readonly StarType[] Types =
    {
        AType,
        FType,
        GType,
        KType,
    };

    public float Probability { get; }
    public string Name { get; }
    public RangeFloat Temperature { get; }
    public RangeFloat Mass { get; }
    public RangeFloat Habitable { get; }

    public StarType(float probability, string name, float minTemp, float maxTemp, float minMass, float maxMass,
        float minHabit, float maxHabit)
    {
        Probability = probability;
        Name = name;
        Temperature = new RangeFloat(minTemp, maxTemp);
        Mass = new RangeFloat(minMass, maxMass);
        Habitable = new RangeFloat(minHabit, maxHabit);
    }
}

