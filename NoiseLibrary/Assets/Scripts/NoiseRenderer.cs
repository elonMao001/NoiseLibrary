using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

using static UnityEngine.Mathf;

public class NoiseRenderer : MonoBehaviour {

    [Header("General")]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private ComputeShader noiseCS;
    private INoiseUpdater noiseUpdater;

    [SerializeField]
    private RenderTexture noiseTexture;

    [SerializeField, Min(1)]
    private int resolution = 1;

    private bool validated;

    [SerializeField]
    private bool active;
    
    public Settings settings;

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
        /*
        if (active) {
            noiseSettings.offset.z += noiseSettings.rateOfChange;
            UpdateNoiseCS();
        }
        */
    }

    private void Init() {
        float aspect = meshRenderer.transform.localScale.y / meshRenderer.transform.localScale.x;
        int resolutionY = (int)Max(resolution * aspect, 1);

        noiseTexture = new RenderTexture(resolution, resolutionY, 1) {
            enableRandomWrite = true,
            filterMode = FilterMode.Point
        };
        
        noiseTexture.Create();
        meshRenderer.sharedMaterial.mainTexture = noiseTexture;

        noiseCS.SetInt("resolution", resolution);
        noiseCS.SetFloats("centre", resolution / 2f, resolutionY / 2f);
    }

    private void UpdateNoiseCS() {
        if (noiseUpdater != null) 
            noiseUpdater.Dispose();
        (noiseUpdater = NoiseUpdaterFactory.CreateNoiseUpdater(settings.noiseSettings)).UpdateCS(noiseCS, "base_");

        noiseCS.SetVector("offset", settings.offset);
        noiseCS.SetTexture(0, "NoiseTexture", noiseTexture);

        noiseCS.Dispatch(0, CeilToInt(noiseTexture.width / 16f), CeilToInt(noiseTexture.height / 16f), 1);
    }

    private void OnValidate() => validated = true;

    public void OnNoiseSettingsUpdated() {
        settings.DetermineNoiseSettings();

        UpdateNoiseCS();
    }

    public void OnDisable() {

    }
}
