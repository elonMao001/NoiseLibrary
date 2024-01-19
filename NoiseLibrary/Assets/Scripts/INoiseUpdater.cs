using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoiseUpdater {
    public void UpdateCS(ComputeShader noiseCS, string prefix);
    public void Dispose();
}
