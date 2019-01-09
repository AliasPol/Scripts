using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkeys : MonoBehaviour {

    public GameObject[] monkeysOn;
    public GameObject[] monkeysOff;

    private void Start() {
        AchivmentMenager.SetAchivment(GPGSls.achievement_learning_is_the_key);
        PlayerPrefs.SetInt("WatchAllTutorials", 1);
        
    }

    public void NextTextMonkeys() {
        for(int i=0; i< monkeysOn.Length; i++) {
            monkeysOn[i].SetActive(true);
            monkeysOff[i].SetActive(false);
        }
    }
}
