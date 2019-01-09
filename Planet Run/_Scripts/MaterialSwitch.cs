using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitch : MonoBehaviour {


    public Material material;

    public void ChangeMaterial()
    {
        MeshRenderer[] mRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer mR in mRenderers) {
            mR.material = material;
        }
    }
}
