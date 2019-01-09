using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenager : MonoBehaviour {

    private void Awake()
    {
        MenagerAudio.Instance.StopAllSound();
    }

    public void StartScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
