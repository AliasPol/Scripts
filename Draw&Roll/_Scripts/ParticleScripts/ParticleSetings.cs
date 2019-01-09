using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSetings : MonoBehaviour {

	// Use this for initialization
	private void Start () {
        StartCoroutine(WaitToDestroy());
	}
	

	private IEnumerator WaitToDestroy() {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
