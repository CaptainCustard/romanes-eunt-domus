using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public NoiseData noiseData;
    public TerrainData terrainData;
    public TextureData textureData;
    public Material terrainMaterial;

    public bool autoGenerate;

    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            this.GenerateMap();
        }
    }

    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);
    }
    public void GenerateMap()
    {
        var mapData = GenerateMapData();

        var meshGenerator = FindObjectOfType<MeshGenerator>();
        meshGenerator.GenerateMesh(mapData.heightMap, this.transform, terrainData.mapHeightMultiplier, terrainData.mapHeightCurve);
    }

    public void GenerateWithRandomSeed()
    {
        noiseData.seed = Random.Range(0, int.MaxValue);
        GenerateMap();
    }

    MapData GenerateMapData()
    {
		float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.lacunarity, noiseData.persistence, terrainData.useFalloff);

		textureData.UpdateMeshHeights(terrainMaterial, terrainData.MinHeight, terrainData.MaxHeight);

		return new MapData (noiseMap);
	}

    void OnValidate()
    {
        textureData.UpdateMeshHeights(terrainMaterial, terrainData.MinHeight, terrainData.MaxHeight);
        textureData.ApplyToMaterial(terrainMaterial);
        
        if (noiseData != null)
        {
            noiseData.OnValuesUpdated -= OnValuesUpdated;
            noiseData.OnValuesUpdated += OnValuesUpdated;
        }

        if (terrainData != null)
        {
            terrainData.OnValuesUpdated -= OnValuesUpdated;
            terrainData.OnValuesUpdated += OnValuesUpdated;
        }

        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }
    }
}

public struct MapData
{
	public readonly float[,] heightMap;

	public MapData (float[,] heightMap)
	{
		this.heightMap = heightMap;
	}
}
