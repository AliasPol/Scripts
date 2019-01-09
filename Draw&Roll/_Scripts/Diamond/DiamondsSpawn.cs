using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondsSpawn : MonoBehaviour {

    public Diamond[] diamonds;


    private void Awake() {
        int rnd1 = Random.Range(0, 7);
        int rnd2 = Random.Range(7, 12);

        diamonds[rnd1].DiamondSpawn();
        diamonds[rnd2].DiamondSpawn();
        
    }
}
