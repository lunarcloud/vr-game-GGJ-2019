public class GameData
{
    public int Seed { get; }

    public PlayerData Player { get; }
    
    public PlanetData[] Planets { get; }
    
    public GameData(int seed)
    {
        // Save the seed
        Seed = seed;

        // Create player data
        Player = new PlayerData();
        
        // Create array for 32 planets
        Planets = new PlanetData[32];
        
        // TODO: Populate origin worlds
        
        // Generate remaining planets
        var gen = new Generator(seed);
        for (var p = 0; p < Planets.Length; ++p)
        {
            Planets[p] = new PlanetData(gen.NextInt());
        }

        // Default location to first planet
        Player.Location = Planets[0];
    }
}
