using UnityEngine;

public class PlanetData
{
    public PlanetData(int seed)
    {
        var rand = new Generator(seed);

        // Select star name
        StarName = NameGenerator.Create(rand.NextInt());

        // Select planet name
        PlanetName = NameGenerator.Create(rand.NextInt());

        // Select location name
        Location = new Vector3(
            rand.NextFloat() * 30f - 15f, 
            rand.NextFloat() * 30f - 15f, 
            rand.NextFloat() * 30f - 15f);
    }

    public string StarName { get; }
    
    public string PlanetName { get; }
    
    public Vector3 Location { get; }
}