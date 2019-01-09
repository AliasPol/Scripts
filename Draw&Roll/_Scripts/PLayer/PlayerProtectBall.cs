using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProtectBall : Player {

    public ParticleSystem[] pS;

    public override IEnumerator WiatForStart() {

        for(int i =0;i<pS.Length; i++) {

            ParticleSystem.TriggerModule psTrigger = pS[i].trigger;
            psTrigger.SetCollider(0, rb.gameObject.GetComponent<CircleCollider2D>());
        }

        yield return new WaitForSeconds(1f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0.5f;
    }


    

}
