using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDelete : MonoBehaviour {

    public int[] speed;


	public void Restart()
    {
        RoundMenager.Instance.Restart();
    }

    public void NextLevel()
    {
        RoundMenager.Instance.NextScene();
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    public void ChangeSceneSpace(int index)
    {
        SpawnSystem.speed = speed[index];
        RoundMenager.Instance.ChangeStage(index);
    }

    
}
