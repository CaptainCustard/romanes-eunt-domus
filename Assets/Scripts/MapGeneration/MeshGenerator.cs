using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
	public void GenerateMesh(float[,] hMap)
	{
		List<Vector3> verts = new List<Vector3>();
		List<int> triangles = new List<int>();

		var xMax = hMap.GetLength(0);
		var zMax = hMap.GetLength(1);

		for (int i = 0; i < xMax; i++)
		{
			for (int j = 0; j < zMax; j++)
			{
				verts.Add(new Vector3(i, hMap[i,j] * 100, j));
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
 
		Vector2[] uvs = new Vector2[verts.Count];
		for (var i = 0; i < uvs.Length; i++) // Give UV coords X,Z world coords
			uvs[i] = new Vector2(verts[i].x, verts[i].z);

		var existingPlane = GameObject.Find("GeneratedPlane");
		if (existingPlane != null) {
			GameObject.DestroyImmediate(existingPlane);
		}
		GameObject plane = new GameObject("GeneratedPlane");
		plane.AddComponent<MeshFilter>();
		plane.AddComponent<MeshRenderer>();
		Mesh procMesh = new Mesh();
		procMesh.vertices = verts.ToArray();
		procMesh.uv = uvs;
		procMesh.triangles = triangles.ToArray();
		procMesh.RecalculateNormals(); // Determines which way the triangles are facing
		plane.GetComponent<MeshFilter>().mesh = procMesh;
	}
}
