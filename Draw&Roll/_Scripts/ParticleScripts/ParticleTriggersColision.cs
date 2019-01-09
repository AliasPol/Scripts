using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleTriggersColision : MonoBehaviour {

    [SerializeField] private ParticleSystem ps;
    public List<ParticleSystem.Particle> particleListEnter = new List<ParticleSystem.Particle>();
    public List<ParticleSystem.Particle> particleListExit = new List<ParticleSystem.Particle>();


    private void OnParticleTrigger() {

        int particleEnterNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particleListEnter);

        for(int i = 0; i < particleEnterNumber; i++) {
            ParticleSystem.Particle p = particleListEnter[i];
            PlayerProtectBall.Instance.PlayerDestroyed();

            p.startColor = Color.black;
            particleListEnter[i] = p;
        }
        
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particleListEnter);


    }

   

}
