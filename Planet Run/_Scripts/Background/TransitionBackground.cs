using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBackground : MonoBehaviour {

    public Vector3 start;

    private void Awake()
    {
        GameObject objSet = RoundMenager.Instance.GetLandStart();
        
        objSet = Instantiate(objSet, start, Quaternion.identity, transform);
        Destroy(objSet.GetComponent<MoveForward>());
    }
}
