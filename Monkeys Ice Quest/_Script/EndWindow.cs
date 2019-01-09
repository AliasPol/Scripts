using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndWindow : MonoBehaviour {

    public Image background;
    public Image plate;
    public GameObject win_elements, loose_elements;
    public GameObject[] stars, stars_particle;
    public AudioClip[] stars_sounds;

    public Text score;
    public Sprite background_win, background_loose;
    public Sprite plate_win, plate_loose;

    void Start()
    {
        background.gameObject.SetActive(false);
    }

    void HideStars()
    {
        for(int i = 0; i < stars.Length; i++)
        {
            stars_particle[i].SetActive(false);
            stars[i].SetActive(false);
        }
    }

    public void ShowWindow(bool win)
    {
        HideStars();
        background.gameObject.SetActive(true);
        background.sprite = win ? background_win : background_loose;
        plate.sprite = win ? plate_win : plate_loose;
        win_elements.SetActive(win);
        loose_elements.SetActive(!win);
        score.text = Score.Instance.yourScore + "";

        if (win) {
            StartCoroutine(ShowStars(Score.Instance.howManyStarsYouGet));
            LevelsMenager.Instance.UnlockedNewLevel();
            LevelsMenager.Instance.StarsSetForLevel(LevelsMenager.Instance.indexCurrentLevel, Score.Instance.howManyStarsYouGet);


            if (Score.Instance.CheckMistakeAchivment(GameManager.Instance.mistakes)) {
                LevelsMenager.Instance.SetArrayWithoutMistake();
            }
            if (Score.Instance.CheckPowersAchivment(GameManager.Instance.AllPowersLeft())) {
                LevelsMenager.Instance.SetArrayPowersNotUse();
            }
            if (Score.Instance.CheckMovesAchivment(GameManager.Instance.moves)) {
                LevelsMenager.Instance.SetArrayMovesNotUse();
            }

        }
    }

    IEnumerator ShowStars(int starsAmount)
    {
        for(int i = 0; i < starsAmount; i++)
        {
            stars[i].SetActive(true);
            stars_particle[i].SetActive(false);
            SoundManager.Instance.PlayClip(stars_sounds[i]);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void Next()
    {
        LevelsMenager.Instance.showMonkey = true;
        LevelsMenager.Instance.indexCurrentLevel++;
        LevelsMenager.Instance.LastPlayedLevelChange(LevelsMenager.Instance.indexCurrentLevel);
        Functions.LoadLoadingScreen();
    }

    public void Repeat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackHome()
    {
        LevelsMenager.Instance.showMonkey = true;
        LevelsMenager.Instance.backFromGame = true;
        LevelsMenager.Instance.sceneName = "MainMenu";
        Functions.LoadLoadingScreen();
        
    }
}
