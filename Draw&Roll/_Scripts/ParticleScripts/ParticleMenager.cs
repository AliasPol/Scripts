using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMenager : MonoBehaviour {

    [SerializeField] private ParticleSystem pS;

    private ParticleSystem.EmissionModule psEmission;

    [SerializeField] private int emisionRateStart;
    [SerializeField] private int emissionRateUpdate;

    private void Awake() {
        psEmission = pS.emission;
    }

    private void Start() {
        StartCoroutine(ChangeEmission());
    }

    private IEnumerator ChangeEmission() {

        var emision = pS.emission;

        while (true) {

            emision.rateOverTime = emisionRateStart;
            yield return new WaitForSeconds(10);

            emisionRateStart += emissionRateUpdate;
        }
    }

    private void Update() {
        
    }
}
