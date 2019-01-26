using UnityEngine;

public class PlayerData
{
    public string PlayerName { get; set; } = CaptainNameGenerator.Create();

    public string ShipName { get; set; } = ShipNameGenerator.Create();

    public long Currency { get; set; } = 50000;

    public PlanetData Location { get; set; }

    public Texture2D LocationTexture { get; set; }

    public Inventory Inventory { get; } = new Inventory();
}