using System;

/// <summary>
/// 3D Perlin Noise class
/// </summary>
public class Perlin3D
{
    public int Depth { get; set; }

    public float Freq { get; set; }

    public int Seed { get; set; }

    public float Value(float x, float y, float z)
    {
        var xa = x * Freq;
        var ya = y * Freq;
        var za = z * Freq;
        var amp = 1.0f;
        var fin = 0.0f;
        var div = 0.0f;

        int i;
        for (i = 0; i < Depth; i++)
        {
            div += 256 * amp;
            fin += Noise(xa, ya, za) * amp;
            amp /= 2;
            xa *= 2;
            ya *= 2;
            za *= 2;
        }

        return fin / div;
    }

    private int NoiseHash(int x, int y, int z)
    {
        var state = 0x2545F4914F6CDD1DUL;
        state += (ulong)Seed;
        state ^= state >> 12;
        state ^= state << 25;
        state ^= state >> 27;
        state += (ulong) x;
        state ^= state >> 12;
        state ^= state << 25;
        state ^= state >> 27;
        state += (ulong)y;
        state ^= state >> 12;
        state ^= state << 25;
        state ^= state >> 27;
        state += (ulong)z;
        state ^= state >> 12;
        state ^= state << 25;
        state ^= state >> 27;
        state *= 0x2545F4914F6CDD1DUL;

        return (int)((state >> 32) & 255);
    }

    private static float LinInter(float x, float y, float s)
    {
        return x + s * (y - x);
    }

    private static float SmoothInter(float x, float y, float s)
    {
        return LinInter(x, y, s * s * (3 - 2 * s));
    }

    private float Noise(float x, float y, float z)
    {
        var xInteger = (int)Math.Floor(x);
        var yInteger = (int)Math.Floor(y);
        var zInteger = (int) Math.Floor(z);
        var xFractional = x - xInteger;
        var yFractional = y - yInteger;
        var zFractional = z - zInteger;
        var n000 = NoiseHash(xInteger, yInteger, zInteger);
        var n100 = NoiseHash(xInteger + 1, yInteger, zInteger);
        var n010 = NoiseHash(xInteger, yInteger + 1, zInteger);
        var n110 = NoiseHash(xInteger + 1, yInteger + 1, zInteger);
        var n001 = NoiseHash(xInteger, yInteger, zInteger + 1);
        var n101 = NoiseHash(xInteger + 1, yInteger, zInteger + 1);
        var n011 = NoiseHash(xInteger, yInteger + 1, zInteger + 1);
        var n111 = NoiseHash(xInteger + 1, yInteger + 1, zInteger + 1);
        var nX00 = SmoothInter(n000, n100, xFractional);
        var nX01 = SmoothInter(n001, n101, xFractional);
        var nX10 = SmoothInter(n010, n110, xFractional);
        var nX11 = SmoothInter(n011, n111, xFractional);
        var nXy0 = SmoothInter(nX00, nX10, yFractional);
        var nXy1 = SmoothInter(nX01, nX11, yFractional);
        var nXyz = SmoothInter(nXy0, nXy1, zFractional);
        return nXyz;
    }
}
