using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour {

    private void Start()
    {
        GameObject pref = RoundMenager.Instance.GetPlanetLandObstacle();
        Vector3 pos = transform.position;


        Instantiate(pref, pos, Quaternion.identity, transform);

        float rnd = Random.Range(0, 100);

        if (rnd > 95) {
            GameObject bullet = RoundMenager.Instance.shootPrefab;
            pos.z += 10;
            int x = Random.Range(-1, 2);
            pos.x = x;
            Instantiate(bullet, pos, Quaternion.identity, transform);
        }

    }
}
