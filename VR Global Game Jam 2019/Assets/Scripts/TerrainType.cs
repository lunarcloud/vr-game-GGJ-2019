using UnityEngine;

public abstract class TerrainType
{
    public static readonly TerrainType BarrenMoon = new BarrenMoonType();
    public static readonly TerrainType IceWorld = new IceWorldType();
    public static readonly TerrainType EarthWorld  = new EarthWorldType();

    public static readonly TerrainType[] Types =
    {
        BarrenMoon,
        IceWorld,
        EarthWorld
    };

    public abstract ITerrainGenerator GetTerrainGenerator(int seed, float temperature);

    private class BarrenMoonType : TerrainType
    {
        public override ITerrainGenerator GetTerrainGenerator(int seed, float temperature)
        {
            return new TerrainGenerator(seed);
        }

        private class TerrainGenerator : ITerrainGenerator
        {
            private readonly ColorMap _map;
            private readonly Perlin3D _noise;

            public TerrainGenerator(int seed)
            {
                _map = new ColorMap
                {
                    { 0f, 0.0f, 0.0f, 0.0f, 1f },
                    { 1f, 0.7f, 0.7f, 0.7f, 1f }
                };

                _noise = new Perlin3D
                {
                    Depth = 5,
                    Freq = 1e-3f,
                    Seed = seed
                };
            }

            public Color GetColor(float x, float y, float z)
            {
                return _map.Entry(_noise.Value(x, y, z));
            }
        }
    }

    private class IceWorldType : TerrainType
    {
        public override ITerrainGenerator GetTerrainGenerator(int seed, float temperature)
        {
            return new TerrainGenerator(seed);
        }

        private class TerrainGenerator : ITerrainGenerator
        {
            private readonly ColorMap _map;
            private readonly Perlin3D _noise;

            public TerrainGenerator(int seed)
            {
                _map = new ColorMap
                {
                    { 0f, 0.6f, 0.6f, 0.6f, 1f },
                    { 1f, 1.0f, 1.0f, 1.0f, 1f }
                };

                _noise = new Perlin3D
                {
                    Depth = 5,
                    Freq = 1e-3f,
                    Seed = seed
                };
            }

            public Color GetColor(float x, float y, float z)
            {
                return _map.Entry(_noise.Value(x, y, z));
            }
        }
    }

    private class EarthWorldType : TerrainType
    {
        public override ITerrainGenerator GetTerrainGenerator(int seed, float temperature)
        {
            return new TerrainGenerator(seed, temperature);
        }

        private class TerrainGenerator : ITerrainGenerator
        {
            private readonly ColorMap _terrainMap;
            private readonly Perlin3D _terrainNoise;
            private readonly ColorMap _cloudMap;
            private readonly Perlin3D _cloudNoise;


            public TerrainGenerator(int seed, float temperature)
            {
                _terrainMap = new ColorMap
                {
                    { 0f, 0.0f, 0.0f, 0.4f, 1f },
                    { 0.58f, 0.0f, 0.0f, 0.6f, 1f },
                    { 0.6f, 0.2f, 0.2f, 0.6f, 1f },
                    { 0.6f, 0.8f, 0.5f, 0.2f, 1f },
                    { 0.61f, 0.8f, 0.5f, 0.0f, 1f },
                    { 0.7f, 0.0f, 0.4f, 0.0f, 1f },
                    { 0.8f, 0.3f, 0.6f, 0.0f, 1f },
                    { 0.9f, 0.5f, 0.4f, 0.0f, 1f },
                    { 0.95f, 0.7f, 0.7f, 0.7f, 1f },
                    { 1f, 1.0f, 1.0f, 1.0f, 1f }
                };

                _terrainNoise = new Perlin3D
                {
                    Depth = 5,
                    Freq = 0.0001f,
                    Seed = seed
                };

                _cloudMap = new ColorMap
                {
                    { 0f, 1.0f, 1.0f, 1.0f, 0f },
                    { 0.6f, 1.0f, 1.0f, 1.0f, 0f },
                    { 0.7f, 1.0f, 1.0f, 1.0f, 1f },
                    { 1f, 0.5f, 0.5f, 0.5f, 1f }
                };

                _cloudNoise = new Perlin3D
                {
                    Depth = 5,
                    Freq = 0.0002f,
                    Seed = seed + 1
                };
            }

            public Color GetColor(float x, float y, float z)
            {
                var terrain = _terrainMap.Entry(_terrainNoise.Value(x, y, z));
                var cloud = _cloudMap.Entry(_cloudNoise.Value(x, y, z));
                return Color.Lerp(terrain, cloud, cloud.a);
            }
        }
    }
}