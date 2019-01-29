using System.Collections.Generic;
using UnityEngine;

public class ColorMap : List<ColorMapEntry>
{
    public void Add(float level, float r, float g, float b, float a)
    {
        Add(new ColorMapEntry(level, new Color(r, g, b, a)));
    }

    public Color Entry(float value)
    {
        // Handle no colors
        if (Count == 0)
            return Color.black;

        // Handle one color
        if (Count == 1)
            return this[0].Color;

        // Handle less than first
        if (value <= this[0].Level)
            return this[0].Color;

        // Linearly interpolate across colors
        for (var i = 1; i < Count; ++i)
        {
            // Skip if not in this range
            if (value > this[i].Level)
                continue;

            // Get lower and upper colors
            var eLower = this[i - 1];
            var eUpper = this[i];

            // Get linear interpolation
            var s = (value - eLower.Level) / (eUpper.Level - eLower.Level);

            // Produce interpolated color
            return Color.Lerp(eLower.Color, eUpper.Color, s);
        }

        // Return last color
        return this[Count - 1].Color;
    }
}