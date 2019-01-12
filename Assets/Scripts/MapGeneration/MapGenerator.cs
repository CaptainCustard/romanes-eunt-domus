using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public int seed;
    public float noiseScale;
    public int octaves;
    public float lacunarity;
    public float persistance;

    public bool autoGenerate;



    public void GenerateMap() {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, lacunarity, persistance);

        var display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);

        var meshGenerator = FindObjectOfType<MeshGenerator>();
        var texture = display.textureRender.sharedMaterial.mainTexture as Texture2D;
        meshGenerator.GenerateMesh(texture);
    }
}
