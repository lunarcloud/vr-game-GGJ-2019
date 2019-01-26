public class PlayerData
{
    public string PlayerName { get; set; } = string.Empty;

    public string ShipName { get; set; } = string.Empty;

    public long Currency { get; set; } = 50000;

    public PlanetData Location { get; set; }

    public Inventory Inventory { get; } = new Inventory();
}