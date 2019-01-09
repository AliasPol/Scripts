using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishScreen : MonoBehaviour {

    public Text finishText;
    public GameObject playButton;
    public GameObject restartButton;

    private string nameOfScene;
    private static FinishScreen instance;
    public static FinishScreen Instance {
        get
        {
            if(instance == null) {
                instance = FindObjectOfType<FinishScreen>();
            }
            return instance;
        }
    }


    private void Start() {
        nameOfScene = SceneManager.GetActiveScene().name;

        if (nameOfScene == "2Balls")
            TwoBallsMenager.Instance.StartLevel();
        else if (nameOfScene == "DontTouchIt")
            DontTouchItMenager.Instance.StartLevel();
    }

    public void FinishRound() {
        finishText.text = "YOU WON!";
        playButton.SetActive(true);
        restartButton.SetActive(false);
        this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        if(nameOfScene == "2Balls" && TwoBallsMenager.Instance.currentLevel == TwoBallsList.SaveList2Ball.TwoBallLevel40) {
            playButton.SetActive(false);
        }
        else if (nameOfScene == "DontTouchIt" && DontTouchItMenager.Instance.currentLevel == DontTouchItList.SaveList.DontTouchItLevel40) {
            playButton.SetActive(false);
        }
    }

    public void RestartLevel() {
        ShowAd();

        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }

    public void StartNewLevel() {
        ShowAd();


        if (nameOfScene == "2Balls") {
            TwoBallsMenager.Instance.currentLevel++;
            XGDatabase.Instance.SaveIntPreferance(TwoBallsMenager.Instance.currentLevel.ToString(), 1);

            int i = XGDatabase.Instance.GetIntPreferance("2BallsLvLUnlocked");
            if(i < (int)TwoBallsMenager.Instance.currentLevel) {
                XGDatabase.Instance.SaveIntPreferance("2BallsLvLUnlocked", i);
                GooglePlayOptions.Instance.CheckLvLAchivment(i + XGDatabase.Instance.GetIntPreferance("DontTouchItLvlUnlocked"));
            }
        }
        else if(nameOfScene == "DontTouchIt") {
            DontTouchItMenager.Instance.currentLevel++;
            XGDatabase.Instance.SaveIntPreferance(DontTouchItMenager.Instance.currentLevel.ToString(), 1);    

            int i = XGDatabase.Instance.GetIntPreferance("DontTouchItLvlUnlocked");
            if (i < (int)DontTouchItMenager.Instance.currentLevel) {
                XGDatabase.Instance.SaveIntPreferance("DontTouchItLvlUnlocked", i);
                GooglePlayOptions.Instance.CheckLvLAchivment(i + XGDatabase.Instance.GetIntPreferance("2BallsLvLUnlocked"));
            }
        }

        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }


    private void ShowAd() {
        if (AdsNormalVideo.Instance != null) {
            AdsNormalVideo.Instance.ShowStandardAd();
        }
    }
}
