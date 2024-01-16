using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using static UnityEngine.Mathf;

public class NoiseRenderer : MonoBehaviour {
    private MeshRenderer meshRenderer;
    
    public NoiseSettings noiseSettings;

    [SerializeField]
    private ComputeShader noiseCS;
    private ComputeBuffer noiseBuffer;

    [SerializeField]
    private RenderTexture noiseTexture;

    [SerializeField, Min(1)]
    private int resolution = 1;

    private bool validated;

    private void Start() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        Init();
        UpdateNoiseCS();
    }

    private void Update() {
        if (validated) {
            Init();
            UpdateNoiseCS();

            validated = false;
        }
    }

    private void Init() {
        float aspect = meshRenderer.transform.localScale.y / meshRenderer.transform.localScale.x;

        noiseTexture = new RenderTexture(resolution, (int)Max(resolution * aspect, 1), 1) {
            enableRandomWrite = true,
            filterMode = FilterMode.Point
        };
        
        noiseTexture.Create();
        meshRenderer.sharedMaterial.mainTexture = noiseTexture;

        noiseCS.SetTexture(0, "NoiseTexture", noiseTexture);
    }

    private void UpdateNoiseCS() {
        noiseCS.Dispatch(0, CeilToInt(noiseTexture.width / 8f), CeilToInt(noiseTexture.height / 8f), 1);
    }

    private void OnValidate() => validated = true;

    public void OnNoiseSettingsUpdated() {
        UpdateNoiseCS();
    }
}
