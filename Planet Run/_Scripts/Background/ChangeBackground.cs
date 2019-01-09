using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour {

    private static ChangeBackground instance;
    public static ChangeBackground Instance
    {
        get
        {
            if(instance == null) {
                instance = FindObjectOfType<ChangeBackground>();
            }
            return instance;
        }
    }


    public MeshRenderer[] obj;

    private void Start()
    {
        ChangeMaterial(RoundMenager.Instance.GetCurrentMaterial());
    }

    public void ChangeMaterial(Material material)
    {
        foreach(MeshRenderer mR in obj) {
            mR.material = material;
        }
    }

}
