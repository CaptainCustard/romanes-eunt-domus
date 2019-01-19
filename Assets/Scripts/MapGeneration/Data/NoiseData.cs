using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class NoiseData : UpdatableData
{    
	public float noiseScale;
	public int octaves;
	[Range(1, 4)]
	public float lacunarity;
	[Range(0, 1)]
	public float persistence;
	public int seed;

	protected override void OnValidate()
	{
		if (octaves < 0)
		{
			octaves = 0;
		}

		base.OnValidate();
	}
}
