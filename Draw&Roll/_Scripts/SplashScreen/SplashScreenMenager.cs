using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenMenager : MonoBehaviour {

	public void Click() {
        SceneManager.LoadSceneAsync(4);
    }
}
