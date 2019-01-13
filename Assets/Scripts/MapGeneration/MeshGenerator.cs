using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
	public void GenerateMesh(float[,] heightMap, Transform transform)
	{
		var verts = new List<Vector3>();
		var triangles = new List<int>();

		var xMax = heightMap.GetLength(0);
		var zMax = heightMap.GetLength(1);

		for (var i = 0; i < xMax; i++)
		{
			for (var j = 0; j < zMax; j++)
			{
				verts.Add(new Vector3(i, heightMap[i,j] * 100, j));
			}
		}

		for (var i = 1; i < xMax; i++)
		{
			for (var j = 1; j < zMax; j++)
			{
				triangles.Add(xMax * i + j); // Top right
				triangles.Add(xMax * i + j - 1); // Bottom right
				triangles.Add(xMax * (i - 1) + j - 1); // Bottom left - First triangle
				triangles.Add(xMax * (i - 1) + j - 1); // Bottom left 
				triangles.Add(xMax * (i - 1) + j); // Top left
				triangles.Add(xMax * i + j); // Top right - Second triangle
			}
		}
 
		var uvs = new Vector2[verts.Count];
		for (var i = 0; i < uvs.Length; i++) // Give UV coords X,Z world coords
		{
			uvs[i] = new Vector2(verts[i].x, verts[i].z);
		}

		var existingPlane = GameObject.Find("GeneratedPlane");
		if (existingPlane != null)
		{
			GameObject.DestroyImmediate(existingPlane);
		}

		var plane = new GameObject("GeneratedPlane");
		plane.AddComponent<MeshFilter>();
		plane.AddComponent<MeshRenderer>();

		var procMesh = new Mesh();
		procMesh.vertices = verts.ToArray();
		procMesh.uv = uvs;
		procMesh.triangles = triangles.ToArray();
		procMesh.RecalculateNormals(); // Determines which way the triangles are facing

		var meshFilter = plane.GetComponent<MeshFilter>();
		meshFilter.mesh = procMesh;
		plane.transform.localScale = transform.localScale;
	}
}
