using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject options_window, quit_window, credits, btn_start_loading;
    public Button btn_start;
    string gameWebPage = "https://www.facebook.com/ArtistsEntertainmentEN/";

    void Start()
    {
        
        GameData.Instance.inMainMenu = true;
        options_window.SetActive(false);
        quit_window.SetActive(false);
        credits.SetActive(false);
        StartCoroutine(ButtonStart());
        AchivmentMenager.Instance.StartScript();
        //GameData.Instance.LoginAndUnlockOfflineAchievements();
        if (!GameData.Instance.backFromGame && !GameData.Instance.backFromMap && LevelsMenager.Instance.backFromGame)
        {
            GameData.Instance.backFromMap = false;
            SoundManager.Instance.PlayMenuMusic();
        }
        if (GameData.Instance.backFromGame)
        {
            GameData.Instance.backFromGame = false;
            ShowRateWindow();

        }

        LevelsMenager.Instance.backFromGame = false;
    }

    IEnumerator ButtonStart()
    {
        if (!GameData.Instance.gameLaunched)
        {
            btn_start.interactable = false;
            while (GameData.Instance.gameDataLoaded)
            {
                yield return null;
            }
            btn_start.interactable = true;
            btn_start_loading.SetActive(false);
        }
        else
        {
            btn_start.interactable = true;
            btn_start_loading.SetActive(false);
        }

        GameData.Instance.SavePlayerProgress();
    }

    void ShowRateWindow()
    {
        if (!GameData.Instance.rateWindowVisible && GameData.Instance.gameLaunchCounter >= 3 && GameData.Instance.timeSpendInGame >= 10 * 60)
        {
            GameData.Instance.rateWindowVisible = true;
            GameObject rateWindow = Instantiate(Resources.Load<GameObject>("RateWindow"));
            RectTransform rateWindowRect = rateWindow.GetComponent<RectTransform>();
            rateWindowRect.SetParent(options_window.transform.parent);
            rateWindowRect.anchorMin = Vector2.zero;
            rateWindowRect.anchorMax = Vector2.one;
            rateWindowRect.localPosition = Vector3.zero;
            rateWindowRect.localScale = Vector3.one;
            rateWindowRect.sizeDelta = Vector2.zero;
        }
    }

    public void StartGame()
    {
        Functions.LoadLevelsMap();
    }

    public void ShowOptions()
    {
        GameData.Instance.inMainMenu = false;
        options_window.SetActive(true);
    }

    public void CloseOptions()
    {
        GameData.Instance.inMainMenu = true;
        options_window.SetActive(false);
    }

    public void GameWebPage()
    {
        Application.OpenURL(gameWebPage);
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
        options_window.SetActive(false);
    }

    public void QuitCredits()
    {
        credits.SetActive(false);
        options_window.SetActive(true);
    }

    public void ShowAchievements()
    {
        AchivmentMenager.Instance.ShowAchievementsUI();
    }

    public void BackToMenu()
    {
        GameData.Instance.inMainMenu = true;
        quit_window.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameData.Instance.inMainMenu)
        {
            quit_window.SetActive(true);
        }
    }
}
