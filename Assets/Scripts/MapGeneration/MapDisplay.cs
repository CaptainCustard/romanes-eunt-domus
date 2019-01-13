using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public void DrawNoiseMap(float[,] noiseMap) {
        var width = noiseMap.GetLength(0);
        var height = noiseMap.GetLength(1);

        var texture = new Texture2D(width, height);

        var colorMap = new Color[width * height];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();

        Renderer renderer = FindObjectOfType<MeshRenderer>();
        var mat = Resources.Load("MapMaterial") as Material;
        mat.mainTexture = texture;
        renderer.sharedMaterial = mat;
    }
}
