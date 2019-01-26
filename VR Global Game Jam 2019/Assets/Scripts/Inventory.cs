using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Inventory : Dictionary<ResourceType, int>
{
    /// <summary>
    /// Get the mass of the inventory
    /// </summary>
    public float Mass => this.Sum(p => p.Key.Mass * p.Value);

    /// <summary>
    /// Get the volume of the inventory
    /// </summary>
    public float Volume => this.Sum(p => p.Key.Volume * p.Value);
}