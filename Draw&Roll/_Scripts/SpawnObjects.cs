using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public GameObject[] superEasyWalls;
    public GameObject[] prefabs;
    public GameObject[] mediumWalls;
    public GameObject[] hardWalls;
    public static SpawnObjects Instance;

    private void Awake() {
        Instance = gameObject.GetComponent<SpawnObjects>();


    }

    public void SpawnNewWalls(float score) {

        float min = score /2;


        float rnd = Random.Range(min, score);

        if(rnd < 300) {
            int i = Random.Range(0, superEasyWalls.Length);
            GameObject newGameObject = Instantiate(superEasyWalls[i], this.transform.position,this.transform.rotation);
        }
        else if (rnd < 800) {
            int i = Random.Range(0, prefabs.Length);
            GameObject newGameObject = Instantiate(prefabs[i], this.transform.position, this.transform.rotation);
        }
        else if(rnd < 1800 ) {
            int i = Random.Range(0, mediumWalls.Length);
            GameObject newGameObject = Instantiate(mediumWalls[i], this.transform.position, this.transform.rotation);

        }
        else if(rnd >= 1800) {
            int i = Random.Range(0, hardWalls.Length);
            GameObject newGameObject = Instantiate(hardWalls[i], this.transform.position, this.transform.rotation);
        }

    }

    public void SpawnFirstWall() {

        GameObject newGameObject = Instantiate(superEasyWalls[0]);
        Vector3 position = this.transform.position;
        newGameObject.transform.position = position;
    }


}
