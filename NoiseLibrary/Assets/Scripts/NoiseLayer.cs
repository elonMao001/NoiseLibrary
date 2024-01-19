using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NoiseLayer {
    public const int maxLayers = 10;
    public const int stride = sizeof(float) * 3;

    public float amplitude, frequency, zoffset;
}
