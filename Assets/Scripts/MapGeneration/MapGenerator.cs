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
    public float persistence;
    public bool autoGenerate;


    public void GenerateMap() {
        var noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, lacunarity, persistence);

        var meshGenerator = FindObjectOfType<MeshGenerator>();
        meshGenerator.GenerateMesh(noiseMap, this.transform);

        var display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    public void GenerateWithRandomSeed() {
        seed = Random.Range(0, int.MaxValue);
        GenerateMap();
    }
}
