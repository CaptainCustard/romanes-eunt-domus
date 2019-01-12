using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public bool autoGenerate;

    public void GenerateMap() {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        var display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);

        var terrain = FindObjectOfType<Terrain>();
        var terrainData = terrain.terrainData;
        terrainData.SetHeights(0, 0, noiseMap);
    }
}
