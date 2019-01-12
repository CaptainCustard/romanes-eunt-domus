using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, int lacunarity, float persistance) {
        float[,] noiseMap = new float[mapWidth, mapHeight];

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
                    float sampleX = x / scale * frequency;
                    float sampleY = y / scale * frequency;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    totalPerlin += perlinValue * Mathf.Pow(persistance, (float)i);
                }

                noiseMap[x, y] = totalPerlin / octaves;
            }
        }

        return noiseMap;
    }
}
