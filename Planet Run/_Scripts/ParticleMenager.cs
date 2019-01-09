using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMenager : MonoBehaviour {

    public static void DestroyParticle (GameObject particle)
    {
        particle.GetComponent<MonoBehaviour>().StartCoroutine(WaitAndDestroy(particle));
    }

    private static IEnumerator WaitAndDestroy(GameObject particle)
    {
        float i = 0;
        while(i < 5f) {
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(particle);
    }
}
