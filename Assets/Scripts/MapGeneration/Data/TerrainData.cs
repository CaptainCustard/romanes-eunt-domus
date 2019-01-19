using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : UpdatableData
{
    public bool useFalloff;
    public float mapHeightMultiplier;
    public AnimationCurve mapHeightCurve;

    public float MinHeight
    {
        get
        {
            return mapHeightMultiplier * mapHeightCurve.Evaluate(0);
        }
    }

    public float MaxHeight
    {
        get
        {
            return mapHeightMultiplier * mapHeightCurve.Evaluate(1);
        }
    }
}
