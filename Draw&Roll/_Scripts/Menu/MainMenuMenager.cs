using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenager : MonoBehaviour {

    public MainMenuButtons buttons;
    public GameObject[] objects;
    bool isOn;


    private void Awake() {
        
        isOn = false;

        DontDestroyOnLoad(gameObject);
    }
    
    private void Update() {
        if(SceneManager.GetActiveScene().buildIndex == 1 && !isOn) {
            foreach(GameObject a in objects) {
                a.SetActive(true);
            }
            buttons.ShowThisCanvas();
            isOn = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4 && !isOn) {
            isOn = true;
            buttons.ShowThisCanvas();
        }
        else if(SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 4 && isOn) {
            foreach (GameObject a in objects) {
                a.SetActive(false);
            }

            isOn = false;
        }
    }

}
