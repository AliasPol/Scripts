using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private void Awake()
    {
        MenagerAudio.Instance.StopAllSound();
        MenagerAudio.Instance.PlaySoundEffect(Sound.EffectList.GameOverScreen);
        SpawnSystem.speed = 6f;
        GameObject[] ob = GameObject.FindGameObjectsWithTag("UI Dont Destroy");
        
        foreach(GameObject obj in ob) {
            Destroy(obj);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(RoundMenager.Instance.gameObject);
    }
}
