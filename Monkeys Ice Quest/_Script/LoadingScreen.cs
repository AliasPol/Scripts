using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        if (LevelsMenager.Instance.sceneName == "MainMenu")
            StartCoroutine(Functions.LoadSceneAsync("MainMenu"));
        else if (CheckTutorial())
            Functions.LoadTutorial();
        else if(LevelsMenager.Instance.indexCurrentLevel > LevelsMenager.Instance.allLevels) {
            Functions.LoadEndScene();
        }
        else
            Functions.LoadGame();
    }

    private bool CheckTutorial() {
        int index = LevelsMenager.Instance.indexCurrentLevel;
        Debug.LogWarning(index);

        switch (index) {
            case 1:
                return true;
                break;
            case 4:
                return true;
                break;
            case 6:
                return true;
                break;
            case 9:
                return true;
                break;
            case 11:
                return true;
                break;
            case 13:
                return true;
                break;
            case 16:
                return true;
                break;
            case 19:
                return true;
                break;
            case 22:
                return true;
                break;
            case 30:
                return true;
                break;
            default:
                return false;
                break;
        }
    }
}
