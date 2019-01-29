using UnityEngine;

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

    private StarType(float probability, string name, float minTemp, float maxTemp, float minMass, float maxMass,
        float minHabit, float maxHabit)
    {
        Probability = probability;
        Name = name;
        Temperature = new RangeFloat(minTemp, maxTemp);
        Mass = new RangeFloat(minMass, maxMass);
        Habitable = new RangeFloat(minHabit, maxHabit);
    }

    public static Color BlackBodyColor(float kelvin)
    {
        var x = kelvin / 1000.0f;
        var x2 = x * x;
        var x3 = x2 * x;
        var x4 = x3 * x;
        var x5 = x4 * x;

        // red
        float red;
        if (kelvin <= 6600)
            red = 1f;
        else
            red = 0.0002889f * x5 - 0.01258f * x4 + 0.2148f * x3 - 1.776f * x2 + 6.907f * x - 8.723f;

        // green
        float green;
        if (kelvin <= 6600)
            green = -4.593e-05f * x5 + 0.001424f * x4 - 0.01489f * x3 + 0.0498f * x2 + 0.1669f * x - 0.1653f;
        else
            green = -1.308e-07f * x5 + 1.745e-05f * x4 - 0.0009116f * x3 + 0.02348f * x2 - 0.3048f * x + 2.159f;

        // blue
        float blue;
        if (kelvin <= 2000f)
            blue = 0f;
        else if (kelvin < 6600f)
            blue = 1.764e-05f * x5 + 0.0003575f * x4 - 0.01554f * x3 + 0.1549f * x2 - 0.3682f * x + 0.2386f;
        else
            blue = 1f;

        // Return the new color
        return new Color(red, green, blue);
    }
}

