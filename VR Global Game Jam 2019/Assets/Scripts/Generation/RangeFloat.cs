public struct RangeFloat
{
    public float Minimum;
    public float Maximum;

    public RangeFloat(float min, float max)
    {
        Minimum = min;
        Maximum = max;
    }

    public float Interpolate(float s)
    {
        return Minimum + s * (Maximum - Minimum);
    }
}