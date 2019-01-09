using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        MenagerAudio.Instance.PlaySoundEffect(Sound.EffectList.ExplosionInSpace);
        GameObject particlePrefab = Resources.Load("ExplosionParticle") as GameObject;
        particlePrefab =  Instantiate(particlePrefab, other.transform.position, Quaternion.identity);


        
        Destroy(other.gameObject);
        Destroy(particlePrefab, 5f);
        Destroy(gameObject);

    }
}
