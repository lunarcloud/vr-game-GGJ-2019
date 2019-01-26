using UnityEngine;

public struct ColorMapEntry
{
    public float Level;
    public Color Color;

    public ColorMapEntry(float level, Color color)
    {
        Level = level;
        Color = color;
    }
}