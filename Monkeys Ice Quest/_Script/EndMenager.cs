using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenager : MonoBehaviour {

    public int levelsCount = 60;
    public Text scoreText;
    public Image trophyImage;
    public Sprite[] trophy;

    private int[] allLevelsStar;


    private void Awake() {
        levelsCount = LevelsMenager.Instance.allLevels;
        allLevelsStar = new int[levelsCount];
        
    }


    private void Start() {
        
        for(int i = 0; i < allLevelsStar.Length; i++) {
            allLevelsStar[i] = LevelsMenager.Instance.StarsGetFromLevel(i+1);
        }

        CountScore();

        AchivmentMenager.SetAchivment(GPGSls.achievement_monkejs_hero);
        PlayerPrefs.SetInt("PassAllLevels", 1);
    }

    private void CountScore() {
        int score1 = 0;
        int score2 = 0;
        int score3 = 0;
        int i = 0;

        foreach (int n in allLevelsStar) {
            
            if(n == 1) {
                Debug.LogWarning(i + "");
                score1++;
            }
            else if(n == 2) {
                score2++;
            }
            else if(n == 3) {
                score3++;
            }
            i++;
        }

        scoreText.text = "Your Score: " + "\n";
        scoreText.text += "1 Star Levels: " + score1 + "\n";
        scoreText.text += "2 Star Levels: " + score2 + "\n";
        scoreText.text += "3 Star Levels: " + score3 + "\n" + "\n";
        if (score1 != 0) {
            scoreText.text += "Get at least 2 stars in all levels to unlock silver cup!";
            //scoreText.text = "You get a bronze trophy. You still need beat " + score1 + " levels to get a silver one!";
            trophyImage.sprite = trophy[0];
            PlayerPrefs.SetInt("Trophy", 1);
        }
        else if(score2 != 0) {
            scoreText.text += "Get 3 stars in all levels to unlock gold cup!";
            //scoreText.text = "You get a silver trophy. You still need beat " + score2 + " levels to get a gold one!";
            trophyImage.sprite = trophy[1];
            PlayerPrefs.SetInt("Trophy", 2);
        }
        else if(score3 != 0) {
            scoreText.text += "You got all stars!";
            //scoreText.text = "You get a gold trophy. You are the beast in the world!";
            trophyImage.sprite = trophy[2];
            PlayerPrefs.SetInt("Trophy", 3);
        }
        else {
            Debug.LogError("ERROR!");
        }
        //scoreText.text = scoreText.text.Replace("\\n", "\n");
        
    }

    public void ExitScene() {
        Functions.LoadMainMenu();
    }
}
