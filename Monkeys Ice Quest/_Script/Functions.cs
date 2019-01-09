using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class Functions {

    public static void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public static void LoadEndScene() {
        SceneManager.LoadScene("EndScene");
    }

    public static void LoadTutorial() {
        SceneManager.LoadScene("GameTutorial");
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadLevelsMap()
    {
        SceneManager.LoadScene("LevelsMap");
    }

    public static void LoadLoadingScreen()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public static IEnumerator LoadSceneAsync(string scene_name, Image loading_indicator = null)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene_name);
        async.allowSceneActivation = true;
        if (loading_indicator != null && loading_indicator.type == Image.Type.Filled)
        {
            while (!async.isDone)
            {
                loading_indicator.fillAmount = async.progress;
                yield return null;
            }
        }

    }

    public static void UnlockAchievement(string achievementID, int achievementIndex = -1)
    {
        Social.ReportProgress(achievementID, 100.0f, (bool success) =>
        {
            if (!success)
                GameData.Instance.standardAchievements.Add(achievementID);
            else
            {
                if (GameData.Instance.standardAchievements.Count == achievementIndex)
                    GameData.Instance.standardAchievements.Clear();
            }
        });
    }

    public static void UnlockIncrementalAchievement(string achievementID, int stepsToUnlock, int achievementIndex = -1)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(achievementID, stepsToUnlock, (bool success) =>
        {
            if (!success)
                GameData.Instance.incrementalAchievements.Add(achievementID);
            else
            {
                if (GameData.Instance.incrementalAchievements.Count == achievementIndex)
                    GameData.Instance.incrementalAchievements.Clear();
            }
        });
    }

    public static void Post_PointsToLeaderBoardAndShow(int points, string leaderboardID)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(points, leaderboardID, (bool success) =>
            {
                if (success)
                {
                    ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboardID);
                }
                else
                {
                    
                }
            });
        }
    }

    public static IEnumerator CheckInternetConnection(System.Action<bool> action)
    {

        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    public static void RateApp()
    {
        Application.OpenURL("market://details?id=com.Artists.MonkejsIce");
    }

    public static IEnumerator MoveUIObject(RectTransform objectToMove, Vector2 startPos, Vector2 endPos, float time)
    {
        if (time > 0)
        {
            float i = 0.0f;
            float rate = 1.0f / time;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                Vector2 currentPos = Vector2.Lerp(startPos, endPos, i);
                objectToMove.anchoredPosition = currentPos;
                yield return null;
            }
        }
        else
            objectToMove.anchoredPosition = endPos;
    }
}
