using System;

public static class ShipNameGenerator
{
    public static string[] Name =
    {
        "Odyssey",
        "Scorpio",
        "Avalon",
        "Titan",
        "Venture Star",
        "Ares",
        "Felicitas",
        "Libertas",
        "Veritas",
        "Apollo"
    };

    public static string Create(int seed)
    {
        // Build random number generator for name
        var rand = new Generator(seed);

        // Select indexes
        var idx = (int)(rand.NextUint() % Name.Length);

        // Combine name
        return Name[idx];
    }

    public static string Create()
    {
        var rand = new Random();
        return Create(rand.Next());
    }
}