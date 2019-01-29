using System;

public struct RangeLong
{
    public long Minimum;
    public long Maximum;

    public RangeLong(long min, long max)
    {
        Minimum = min;
        Maximum = max;
    }

    public long Interpolate(float s)
    {
        return (long)(Math.Round(Minimum + s * (Maximum - Minimum)));
    }
}