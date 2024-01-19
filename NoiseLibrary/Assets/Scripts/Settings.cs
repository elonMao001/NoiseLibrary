using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Settings", order = 0)]
public class Settings : ScriptableObject {
    [Header("Settings")]
    public NoiseType noiseType;
    public enum NoiseType {
        Regular, Rigid
    }
    public Vector3 offset;
    public float rateOfChange;

    [SerializeField, SerializeReference]
    public INoiseSettings noiseSettings;
    [HideInInspector]
    public RegularNoiseSettings regularNoiseSettings;
    [HideInInspector]
    public RigidNoiseSettings rigidNoiseSettings;

    public interface INoiseSettings { }

    [Serializable]
    public class RegularNoiseSettings : INoiseSettings {
        public float amplitude = 1f;
        public float frequency = 0.2f;
        [Range(0f, 2f)]
        public float persistence = 0.5f;
        [Range(1f, 5f)]
        public float lacunarity = 2f;
        [Range(1, NoiseLayer.maxLayers)]
        public int layers = 1;
    }
    [Serializable]
    public class RigidNoiseSettings : INoiseSettings {
        public float amplitude = 1f;
        public float frequency = 0.2f;
        [Range(0f, 2f)]
        public float persistence = 0.5f;
        [Range(1f, 5f)]
        public float lacunarity = 2f;
        [Range(1, NoiseLayer.maxLayers)]
        public int layers = 1;
        public float steepness = 1f;
        [Range(-2f, 2f)]
        public float noiseValueOffset = 1f;
    }
    public void DetermineNoiseSettings() {
        switch (noiseType) {
            case NoiseType.Regular:
                noiseSettings = regularNoiseSettings;
                break;
            case NoiseType.Rigid:
                noiseSettings = rigidNoiseSettings;
                break;
        }
    }
}
