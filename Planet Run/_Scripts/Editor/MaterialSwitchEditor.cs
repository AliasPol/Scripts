using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MaterialSwitch))]
public class MaterialSwitchEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MaterialSwitch mS = (MaterialSwitch)target;

        if (GUILayout.Button("SwitchMaterial")) {
            mS.ChangeMaterial();
        }
    }
}
