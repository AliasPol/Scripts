using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour {

    private Vector3 spawn = new Vector3(-0.44f, 0.118f, -0.004f);

	// Use this for initialization
	void Start () {
        GameObject particle = Instantiate(Resources.Load("HotDogParticle"), this.transform) as GameObject;
        particle.transform.localPosition = spawn;
    }
	
	
}
