using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public void GenerateMesh(Texture2D hMap)
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> triangles = new List<int>();
 
        // Bottom left section of the map, other sections are similar
        for (int i = 0; i < 250; i++)
        {
            for (int j = 0; j < 250; j++)
            {
                // Add each new vertex in the plane
                verts.Add(new Vector3(i, hMap.GetPixel(i,j).grayscale * 100, j));
                // Skip if a new square on the plane hasn't been formed
                if (i == 0 || j == 0) continue;
                // Adds the index of the three vertices in order to make up each of the two tris
                triangles.Add(250 * i + j); // Top right
                triangles.Add(250 * i + j - 1); // Bottom right
                triangles.Add(250 * (i - 1) + j - 1); // Bottom left - First triangle
                triangles.Add(250 * (i - 1) + j - 1); // Bottom left 
                triangles.Add(250 * (i - 1) + j); // Top left
                triangles.Add(250 * i + j); // Top right - Second triangle
            }
        }
 
        Vector2[] uvs = new Vector2[verts.Count];
        for (var i = 0; i < uvs.Length; i++) // Give UV coords X,Z world coords
            uvs[i] = new Vector2(verts[i].x, verts[i].z);

        Mesh procMesh = new Mesh();
        procMesh.vertices = verts.ToArray();
        procMesh.uv = uvs;
        procMesh.triangles = triangles.ToArray();
        procMesh.RecalculateNormals(); // Determines which way the triangles are facing
        var meshFilter = FindObjectOfType<MeshFilter>();
        meshFilter.mesh = procMesh;
    }
}
