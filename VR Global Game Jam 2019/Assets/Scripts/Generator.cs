using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Pseudorandom number generator
/// </summary>
public class Generator
{
    /// <summary>
    /// Current state
    /// </summary>
    private ulong _state = 0x2545F4914F6CDD1DUL;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="seeds">Random seeds</param>
    public Generator(params int[] seeds)
    {
        Add(seeds);
    }

    /// <summary>
    /// Add additional seed data
    /// </summary>
    /// <param name="seeds">Random seeds</param>
    public void Add(params int[] seeds)
    {
        foreach (var seed in seeds)
        {
            _state += (ulong) seed;
            Cycle();
        }
    }

    /// <summary>
    /// Generate an int
    /// </summary>
    /// <returns>Random int</returns>
    public int NextInt()
    {
        return (int) Cycle();
    }

    /// <summary>
    /// Generate a uint
    /// </summary>
    /// <returns>Random uint</returns>
    public uint NextUint()
    {
        return Cycle();
    }

    /// <summary>
    /// Generate a float
    /// </summary>
    /// <returns>Random normalized fload</returns>
    public float NextFloat()
    {
        return Cycle() / (float) uint.MaxValue;
    }

    /// <summary>
    /// Generate a random item from a collection
    /// </summary>
    /// <typeparam name="T">Type of items</typeparam>
    /// <param name="items">Collection of items</param>
    /// <returns>Randomly selected item</returns>
    public T NextFrom<T>(ICollection<T> items) where T : IProbability
    {
        // Select the value within the probability spread
        var val = NextFloat() * items.Sum(i => i.Probability);

        // Loop selecting the item at the given value
        foreach (var item in items)
        {
            var prob = item.Probability;
            if (val <= prob) return item;
            val -= prob;
        }

        // Should not happen, but just pick the last
        return items.Last();
    }

    /// <summary>
    /// Generate a random value in a range
    /// </summary>
    /// <param name="range">Range for value</param>
    /// <returns>Random value</returns>
    public float NextFrom(RangeFloat range)
    {
        return range.Interpolate(NextFloat());
    }

    /// <summary>
    /// Cycle the pseudorandom number generator
    /// </summary>
    /// <returns>Next 64-bit output</returns>
    private uint Cycle()
    {
        var x = _state;
        x ^= x >> 12;
        x ^= x << 25;
        x ^= x >> 27;
        _state = x;
        return (uint)((x * 0x2545F4914F6CDD1DUL) >> 32);
    }
}
