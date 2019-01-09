using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public  float score2Stars;
    public  float score3Stars;

    public  float scoreToGet;

    [HideInInspector] public  float yourScore;
    [HideInInspector] public int howManyStarsYouGet;

    private static Score instance;
    public static Score Instance {
        get
        {
            if (instance == null) {
                instance = FindObjectOfType<Score>();
            }
            return instance;
        }
    }

    private int startMistake;
    private int startPowers;
    private int startMoves;

    public void SetScoreToGet(int moves, int mistake, int powers) {

        scoreToGet = moves * 100 + mistake * 200 + powers * 50;
        startMistake = mistake;
        startPowers = powers;
        startMoves = moves;
    }

    public void YourScore(int moves, int mistake, int powers) {
        yourScore = moves * 100 + mistake * 200 + powers * 50;
        CheckStars();
    }

    private void CheckStars() {

        if(score3Stars <= yourScore) {
            howManyStarsYouGet = 3;
        }
        else if(score2Stars <= yourScore) {
            howManyStarsYouGet = 2;
        }
        else {
            howManyStarsYouGet = 1;
        }

        Debug.LogError(yourScore + "     " + howManyStarsYouGet);
    }

    public bool CheckMistakeAchivment(int mistake) {
        if(mistake >= startMistake) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool CheckPowersAchivment(int powers) {
        if(powers >= startPowers) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool CheckMovesAchivment(int moves) {
        if (moves >= startMoves) {
            return true;
        }
        else {
            return false;
        }
    }
}
