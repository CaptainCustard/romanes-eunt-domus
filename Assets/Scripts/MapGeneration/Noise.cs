using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float lacunarity, float persistance) {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        float normalize = Enumerable.Range(0, octaves).Sum(n => Mathf.Pow(persistance, n));

        System.Random rng = new System.Random(seed);
        Vector2[] octaveOffsets = Enumerable.Range(0, octaves).Select(x => new Vector2(rng.Next(-100000, 100000), rng.Next(-100000, 100000))).ToArray();

        if (scale <= 0)
        {
            scale = 0.00001f;
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float totalPerlin = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float frequency = Mathf.Pow(lacunarity, (float)i);
                    float sampleX = x / scale * frequency + octaveOffsets[i].x;
                    float sampleY = y / scale * frequency + octaveOffsets[i].y;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    totalPerlin += perlinValue * Mathf.Pow(persistance, (float)i);
                }

                noiseMap[x, y] = ((totalPerlin / normalize) + 1) / 2;
            }
        }

        return noiseMap;
    }
}
