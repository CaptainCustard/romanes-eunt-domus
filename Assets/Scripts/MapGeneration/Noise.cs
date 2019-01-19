using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float lacunarity, float persistence, bool useFalloff) {
        var noiseMap = new float[mapWidth, mapHeight];
        var falloffMap = new float[mapWidth, mapHeight];
        var normalize = Enumerable.Range(0, octaves).Sum(n => Mathf.Pow(persistence, n));

        if (useFalloff)
        {
            falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
        }

        var rng = new System.Random(seed);
        var octaveOffsets = Enumerable.Range(0, octaves).Select(x => new Vector2(rng.Next(-100000, 100000), rng.Next(-100000, 100000))).ToArray();

        if (scale <= 0)
        {
            scale = 0.00001f;
        }

        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidth; x++)
            {
                var totalPerlin = 0f;
                for (var i = 0; i < octaves; i++)
                {
                    var frequency = Mathf.Pow(lacunarity, (float)i);
                    var sampleX = x / scale * frequency + octaveOffsets[i].x;
                    var sampleY = y / scale * frequency + octaveOffsets[i].y;
                    var perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    totalPerlin += perlinValue * Mathf.Pow(persistence, (float)i);
                }
                var value = Mathf.Clamp01((((totalPerlin / normalize) + 1) / 2) - falloffMap[x, y]);
                noiseMap[x, y] = value;
            }
        }

        return noiseMap;
    }
}
