public class PlanetCompositionType : IProbability
{
    public static readonly PlanetCompositionType NickelIron = new PlanetCompositionType(0.6f, "NickelIron");
    public static readonly PlanetCompositionType Silicate = new PlanetCompositionType(0.3f, "Silicate");

    public static readonly PlanetCompositionType[] Types =
    {
        NickelIron,
        Silicate
    };

    private PlanetCompositionType(float prob, string name)
    {
        Probability = prob;
        Name = name;
    }

    public float Probability { get; }
    public string Name { get; }
}