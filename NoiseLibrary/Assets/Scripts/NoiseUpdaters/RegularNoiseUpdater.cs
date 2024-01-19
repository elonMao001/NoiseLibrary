using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using NoiseSettings = Settings.RegularNoiseSettings;

public class RegularNoiseUpdater : INoiseUpdater {
    private NoiseSettings noiseSettings;
    private ComputeBuffer noiseLayerBuffer;

    public RegularNoiseUpdater(NoiseSettings noiseSettings) {
        this.noiseSettings = noiseSettings;
    }
    
    public void UpdateCS(ComputeShader noiseCS, string prefix) {
        
    }

    public void Dispose() {
        noiseLayerBuffer.Dispose();
    }
}
