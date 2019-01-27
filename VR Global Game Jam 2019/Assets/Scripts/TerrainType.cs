using UnityEngine;

public abstract class TerrainType : IProbability
{
    public static readonly TerrainType BarrenRock = new BarrenRockType();
    public static readonly TerrainType IceWorld = new IceWorldType();
    public static readonly TerrainType EarthWorld  = new EarthWorldType();

    public static readonly TerrainType[] Types =
    {
        BarrenRock,
        IceWorld,
        EarthWorld
    };

    public abstract float Probability { get; }
    public abstract string Name { get; }
    public abstract ITerrainGenerator GetTerrainGenerator(int seed, float temperature, float diameter);

    private class BarrenRockType : TerrainType
    {
        public override float Probability => .1f;
        public override string Name => "Barren Rock";
        public override string ToString() => Name;

        public override ITerrainGenerator GetTerrainGenerator(int seed, float temperature, float diameter)
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
        public override float Probability => .2f;
        public override string Name => "Ice World";
        public override string ToString() => Name;

        public override ITerrainGenerator GetTerrainGenerator(int seed, float temperature, float diameter)
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
        public override float Probability => 1f;
        public override string Name => "Earth-Like World";
        public override string ToString() => Name;

        public override ITerrainGenerator GetTerrainGenerator(int seed, float temperature, float diameter)
        {
            return new TerrainGenerator(seed, temperature, diameter);
        }

        private class TerrainGenerator : ITerrainGenerator
        {
            private readonly ColorMap _temperateMap;
            private readonly ColorMap _polarMap;
            private readonly Perlin3D _terrainNoise;
            private readonly Perlin3D _polarNoise;
            private readonly ColorMap _cloudMap;
            private readonly Perlin3D _cloudNoise;
            private readonly float _temperature;
            private readonly float _zMul;


            public TerrainGenerator(int seed, float temperature, float diameter)
            {
                _temperature = temperature;

                _zMul = -30 / diameter;

                _temperateMap = new ColorMap
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

                _polarMap = new ColorMap
                {
                    { 0f, 0.6f, 0.6f, 0.6f, 1f },
                    { 0.6f, 0.6f, 0.6f, 0.6f, 1f },
                    { 0.6f, 0.75f, 0.75f, 0.75f, 1f },
                    { 0.61f, 0.7f, 0.7f, 0.7f, 1f },
                    { 0.95f, 0.7f, 0.7f, 0.7f, 1f },
                    { 1f, 1.0f, 1.0f, 1.0f, 1f }
                };

                _terrainNoise = new Perlin3D
                {
                    Depth = 5,
                    Freq = 0.0001f,
                    Seed = seed
                };

                _polarNoise = new Perlin3D
                {
                    Depth = 5,
                    Freq = 0.00003f,
                    Seed = seed+1
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
                    Seed = seed + 2
                };
            }

            public Color GetColor(float x, float y, float z)
            {
                var isPolar = _temperature + Mathf.Abs(z) * _zMul + _polarNoise.Value(x, y, z) * 20 < 0;
                var terrain = (isPolar ? _polarMap : _temperateMap).Entry(_terrainNoise.Value(x, y, z));
                var cloud = _cloudMap.Entry(_cloudNoise.Value(x, y, z));
                return Color.Lerp(terrain, cloud, cloud.a);
            }
        }
    }
}