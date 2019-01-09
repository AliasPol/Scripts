using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public Vector3 rotation;
	
	// Update is called once per frame
	void Update () {
        Vector3 curRotation = transform.eulerAngles;
        curRotation = rotation * Time.deltaTime + curRotation;
        transform.eulerAngles = curRotation;
	}
}
