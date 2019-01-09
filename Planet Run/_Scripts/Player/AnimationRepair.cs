using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRepair : MonoBehaviour {

    private float x;
    private float y;
    private float z=0;
    private Quaternion q;

    private void Awake()
    {
        x = transform.position.x;
        q = transform.rotation;
        y = transform.localPosition.y;

            z = transform.localPosition.z;
    }

    void LateUpdate () {
        Vector3 pos = transform.localPosition;


            pos.z = z;

        pos.y = y;
        pos.x = x;
        transform.localPosition = pos;
        transform.rotation = q;
	}
}
