using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GooglePlayOptions : MonoBehaviour {

    private static GooglePlayOptions instance;
    public static GooglePlayOptions Instance {
        get
        {
            if(instance == null) {
                instance = Instantiate(Resources.Load<GooglePlayOptions>("Singletons/GooglePlayOptions")) as GooglePlayOptions;
                instance.name = "GooglePlayOptions";
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private bool isConnected = false;

    public bool ConnectToGoogleServices() {

        if (!isConnected) {
            PlayGamesPlatform.Activate();
            

            Social.localUser.Authenticate((bool success) => {
                isConnected = success;
                Debug.LogWarning(isConnected + "   Polaczenie z google play service   " + success);
            });
        }
        return isConnected;
    }

    public void ShowAchievement() {

        if (isConnected) {
            Social.ShowAchievementsUI();
        }
        else if (ConnectToGoogleServices()) {
            Social.ShowAchievementsUI();
        }
        else {
            Debug.Log("Failed to connect google play");
        }
    }

    public void ShowLeaderboard() {

        if (isConnected) {
            Social.ShowLeaderboardUI();
        }
        else if (ConnectToGoogleServices()) {
            Social.ShowLeaderboardUI();
        }
        else {
            Debug.Log("Failed to connect google play");
        }
    }

    public void SetLeaderboard(int newScore) {
        Social.ReportScore(newScore, LineRiderGoogle.leaderboard_draw__roll_ball_high_score, (bool success) => {
            if (success)
                Debug.Log("SUCCES REPORT high score");
            else {
                Debug.Log("Failed report high score");
            }
        });
    }

    public void SetLeaderboardDiamond(int diamond) {
        Social.ReportScore(diamond, LineRiderGoogle.leaderboard_draw__roll_ball_diamonds, (bool success) => {
            if (success)
                Debug.Log("SUCCES REPORT diamond");
            else {
                Debug.Log("Failed report diamond score");
            }
        });
    }
	

    public void SetAchivment(string achvName) {
        Social.ReportProgress(achvName,100.0f, (bool success) => {
            if (success) {
                Debug.Log("SUCCES Unlock");
                PlayerPrefs.SetInt(achvName, 1);
            }
        });
    }

    public void CheckScoreAchivment(int score) {

        int i = PlayerPrefs.GetInt("ScoreA", 0);

        if(score >= 4000 && i < 5) {
            SetAchivment(LineRiderGoogle.achievement_this_game_is_easy);
            PlayerPrefs.SetInt("ScoreA", 5);
        }
        if(score >= 2000 && i < 4) {
            SetAchivment(LineRiderGoogle.achievement_i_am_the_master);
            PlayerPrefs.SetInt("ScoreA", 4);
        }
        if(score >= 1500 && i < 3) {
            SetAchivment(LineRiderGoogle.achievement_no_one_can_match_me);
            PlayerPrefs.SetInt("ScoreA", 3);
        }
        if(score >= 1000 && i < 2) {
            SetAchivment(LineRiderGoogle.achievement_im_getting_better);
            PlayerPrefs.SetInt("ScoreA", 2);
        }
        if(score >= 500 && i < 1) {
            SetAchivment(LineRiderGoogle.achievement_beginner);
            PlayerPrefs.SetInt("ScoreA", 1);
        }
    }

    public void CheckLvLAchivment(int lvlUnlocked) {
        switch (lvlUnlocked) {
            case 20:
                SetAchivment(LineRiderGoogle.achievement_beginner_explorer);
                break;
            case 40:
                SetAchivment(LineRiderGoogle.achievement_advanced_explorer);
                break;
            case 60:
                SetAchivment(LineRiderGoogle.achievement_challenge_seeker);
                break;
            case 80:
                SetAchivment(LineRiderGoogle.achievement_level_destroyer);
                break;
        }
    }

    public void CheckRollAchievment(int roll) {

        switch (roll) {
            case 1:
                SetAchivment(LineRiderGoogle.achievement_gift_for_you);
                break;
            case 10:
                SetAchivment(LineRiderGoogle.achievement_collector);
                break;
            case 50:
                SetAchivment(LineRiderGoogle.achievement_you_like_gifts);
                break;
        }
    }
}
