using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenager : MonoBehaviour {

    public float moveToNext;
    public Text scoreText;
    public Text diamondsValue;
    public bool spawnWalls = true;

    private Vector2 checkVector;
    private float score = 0f;
    public static GameMenager Instance;
    private int currentDiamonds = 0;

    private void Awake() {
        Instance = gameObject.GetComponent<GameMenager>();
    }

    private void Start() {
        checkVector = this.gameObject.transform.position;


        if(SpawnObjects.Instance != null)
            SpawnFirstWall();
    }

    private void FixedUpdate() {

        if (spawnWalls && checkVector.x < this.gameObject.transform.position.x) {
            SpawnNewWall();
        }
    }

    private void SpawnNewWall() {
        checkVector.x = checkVector.x + moveToNext;

        SpawnObjects.Instance.SpawnNewWalls(score);
    }

    private void SpawnFirstWall() {
        checkVector.x = checkVector.x + moveToNext;

        SpawnObjects.Instance.SpawnFirstWall();
    }


    public void CalculateHighScore(float lenght) {
        score = lenght;
        scoreText.text = "SCORE: " +(int) lenght;
    }

    public void StartEndScreen() {

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        EndScreen.Instance.EndScreenStart((int)score, highScore, currentDiamonds);

    }

    

    public void AddDiamonds(int value) {
        currentDiamonds += value;

        diamondsValue.text = currentDiamonds + "";
    }

    public void DubleDiamonds() {
        
        currentDiamonds = currentDiamonds * 2;
        EndScreen.Instance.RewardedVideo(currentDiamonds);
        EndScreen.Instance.adButton.interactable = false;
    }
}
