using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyButton : MonoBehaviour {

    public Sprite[] trophy;
    public Image imageButton;



    private void Awake()
    {
        if(LevelsMenager.Instance.unlockedLevels >= LevelsMenager.Instance.allLevels)
        {
            int unlocked = PlayerPrefs.GetInt("Trophy", 1);

            if (unlocked < 3) {
                CheckTrophy();
            }
            else {
                imageButton.sprite = trophy[2];
            }
            
        }
        else
        {
            Debug.Log("Dont Unlocked All levels");
            gameObject.SetActive(false);
            
        }
    }

    public void TrophyScene() {
        Functions.LoadEndScene();
    }


    public void CheckTrophy() {
        int levelsCount = LevelsMenager.Instance.allLevels;
        int[] allLevelsStar = new int[levelsCount];

        for (int i = 0; i < allLevelsStar.Length; i++) {
            allLevelsStar[i] = LevelsMenager.Instance.StarsGetFromLevel(i + 1);
        }

        CountScore(allLevelsStar);

    }

    private void CountScore(int[] allLevelsStar) {
        int score1 = 0;
        int score2 = 0;
        int score3 = 0;
        int i = 0;

        foreach (int n in allLevelsStar) {

            if (n == 1) {
                score1++;
            }
            else if (n == 2) {
                score2++;
            }
            else if (n == 3) {
                score3++;
            }
            i++;
        }

        if (score1 != 0) {
            imageButton.sprite = trophy[0];
            PlayerPrefs.SetInt("Trophy", 1);
        }
        else if (score2 != 0) {
            imageButton.sprite = trophy[1];
            PlayerPrefs.SetInt("Trophy", 2);
        }
        else if (score3 != 0) {
            imageButton.sprite = trophy[2];
            PlayerPrefs.SetInt("Trophy", 3);
        }
    }
}
