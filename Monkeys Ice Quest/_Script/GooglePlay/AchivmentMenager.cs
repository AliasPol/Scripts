using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivmentMenager : MonoBehaviour {

    int passFirstLevel;
    int watchAllTutorials;
    int withoutBreakingAnyBlock;
    int use100Powers;
    int passAllLevels;
    int useRotate10Times;
    int pass5LevelsWithouthPowers;
    int pass10LevlsWithoutTappingIceBlock;

    private bool isConnected = false;

    private static AchivmentMenager instance;
    public static AchivmentMenager Instance {
        get
        {
            if (instance == null) {
                instance = Instantiate(Resources.Load<AchivmentMenager>("AchivmentMenager")) as AchivmentMenager;
                DontDestroyOnLoad(instance);
                instance.name = "AchivmentMenager";

            }
            return instance;
        }
    }


    public void StartScript() {
        InitPlayServices();
        LoadAchivments();
        CheckAchivments();
    }

    private bool ConnectToGoogleServices() {

        if (!isConnected) {


            Social.localUser.Authenticate((bool success) => {
                isConnected = success;
            });
        }
        return isConnected;
    }

    private void InitPlayServices() {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .EnableSavedGames()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) => {
            isConnected = success;
        });


    }

    private void LoadAchivments() {
        passFirstLevel = PlayerPrefs.GetInt("PassFirstLevel",0);
        watchAllTutorials = PlayerPrefs.GetInt("WatchAllTutorials", 0);
        withoutBreakingAnyBlock = PlayerPrefs.GetInt("BreakingAnyBlock", 0);
        use100Powers = PlayerPrefs.GetInt("Use100Powers", 0);
        passAllLevels = PlayerPrefs.GetInt("PassAllLevels", 0);
        useRotate10Times = PlayerPrefs.GetInt("UseRotate10Times",0);
        pass5LevelsWithouthPowers = PlayerPrefs.GetInt("Pass5Levels", 0);
        pass10LevlsWithoutTappingIceBlock = PlayerPrefs.GetInt("Pass10Levels", 0);
    }

    private void CheckAchivments() {

        if (ConnectToGoogleServices()) {
            if (passFirstLevel == 1) {
                SetAchivment(GPGSls.achievement_nice_to_see_you);
            }
            if (watchAllTutorials == 1) {
                SetAchivment(GPGSls.achievement_learning_is_the_key);
            }
            if (withoutBreakingAnyBlock == 1) {
                SetAchivment(GPGSls.achievement_mistakes_not_here);
            }
            if (use100Powers >= 100) {
                SetAchivment(GPGSls.achievement_powerful);
            }
            if (passAllLevels == 1) {
                SetAchivment(GPGSls.achievement_monkejs_hero);
            }
            if (useRotate10Times >= 10) {
                SetAchivment(GPGSls.achievement_pirouette);
            }
            if (pass5LevelsWithouthPowers >= 5) {
                SetAchivment(GPGSls.achievement_make_it_simple);
            }
            if (pass10LevlsWithoutTappingIceBlock >= 10) {
                SetAchivment(GPGSls.achievement_untouchable);
            }
        }
    }

    public static void SetAchivment(string achvName) {
        Social.ReportProgress(achvName, 100.0f, (bool success) => {
            if (success) {
                Debug.Log("SUCCES Unlock");
            }
            else {
                Debug.Log("Fail to Unlock");
            }
        });
    }

    public void ShowAchievementsUI() {
        if (!isConnected) {
            ConnectToGoogleServices();
        }
        Social.ShowAchievementsUI();
    }

}
