using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoiseRenderer))]
public class NoiseEditor : Editor {
    private NoiseRenderer noiseRenderer;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        DrawSettingsEditor(noiseRenderer.settings, noiseRenderer.OnNoiseSettingsUpdated);
    }

    private void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated) {
        using (var check = new EditorGUI.ChangeCheckScope()) {
            Editor editor = CreateEditor(settings);
            editor.OnInspectorGUI();
            
            if (check.changed) {
                if (onSettingsUpdated != null) {
                    onSettingsUpdated();
                }
            }
        }
    }

    private void OnEnable() {
        noiseRenderer = (NoiseRenderer)target;
    }
}
