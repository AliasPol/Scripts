using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseWindow : MonoBehaviour {

    public AudioClip show_pause_window, hide_pause_window;

    void OnEnable()
    {
        SoundManager.Instance.PlayClip(show_pause_window);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        SoundManager.Instance.PlayClip(hide_pause_window);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Repeat()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackHome()
    {
        Time.timeScale = 1;
        LevelsMenager.Instance.showMonkey = true;
        LevelsMenager.Instance.backFromGame = true;
        LevelsMenager.Instance.sceneName = "MainMenu";
        Functions.LoadLoadingScreen();
    }


}
