using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "NoiseSettings", order = 0)]
public class NoiseSettings : ScriptableObject {
    [Header("Noise Settings")]
    public float amplitude = 0;
    public float frequency = 0;
}
