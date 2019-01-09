using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceLessCutter : MonoBehaviour {

    public List<GameObject> asteroidList = new List<GameObject>();


    public void GetObjectsToList()
    {
        
        int i = asteroidList.Count / 2;
        int index = 0;
        while(index < i) {
            int rnd = Random.Range(0, asteroidList.Count);
            asteroidList.Remove(asteroidList[rnd]);
            index++;
        }

        foreach(GameObject z in asteroidList) {
            z.SetActive(false);
        }
    }

    public void SwitchOn()
    {
        if (asteroidList.Count > 0) {
            int rnd = Random.Range(0, asteroidList.Count);
            asteroidList[rnd].SetActive(true);
            asteroidList.Remove(asteroidList[rnd]);
        }
        else {
            Destroy(this);
        }
    }
}
