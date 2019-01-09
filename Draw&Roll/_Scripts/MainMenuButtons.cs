using Game.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {
    public MenuMenager menuMenager;
    public Menu menu;


    private void Start() {
        GooglePlayOptions.Instance.ConnectToGoogleServices();
    }

    public void ChangeScene(int index) {
        AdsNormalVideo.Instance.ShowStandardAd();


        if(index == 1)
            ShowThisCanvas();
        
        SceneManager.LoadScene(index);
    }



    public void ShowThisCanvas() {
        menuMenager.OnlyChangeMenu(menu);
    }

    public void CloseThisCanvas() {
        menuMenager.ClosePopupMenu(menu);
    }

    public void Leaderboard() {
        GooglePlayOptions.Instance.ShowLeaderboard();
    }

    public void Achivment() {
        GooglePlayOptions.Instance.ShowAchievement();
    }

    public void Facebook() {
        Application.OpenURL("https://www.facebook.com/Xintage-Games-543199539376076");
    }

    public void RateGame() {
        Application.OpenURL("market://details?id=com.XinTage.LineRider");
    }
}
