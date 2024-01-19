using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseUpdaterFactory {
    public static INoiseUpdater CreateNoiseUpdater(Settings.INoiseSettings noiseSettings) {
        switch (noiseSettings) {
            case Settings.RegularNoiseSettings:
                return new RegularNoiseUpdater((Settings.RegularNoiseSettings)noiseSettings);
        }
        
        return null;
    }
} 
