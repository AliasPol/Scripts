using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    public static EndScreen Instance;

    public Text yourScore;
    public Text highScore;
    public Text gemsValue;

    public Button adButton;

	// Use this for initialization
	private void Awake () {
        Instance = GetComponent<EndScreen>();
	}
	

	public void EndScreenStart(int score, int high_Score, int collect_gems) {
        int allCollect = PlayerPrefs.GetInt("AllGems", 0);
        allCollect += collect_gems;
        PlayerPrefs.SetInt("AllGems", allCollect);
        DiamondsMengerMenu.Instance.CheckGemsAchivment(allCollect);


        this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        yourScore.text = score + "";

        int currentGems = PlayerPrefs.GetInt("Gems", 0);

        gemsValue.text = currentGems + " + " + collect_gems;

        PlayerPrefs.SetInt("Gems", (collect_gems + currentGems));
        if (high_Score >= score) {
            highScore.text = high_Score + "";
        }
        else {
            GooglePlayOptions.Instance.SetLeaderboard(score);
            highScore.text = score + "";
            PlayerPrefs.SetInt("HighScore", score);
        }      
        StartParticle();

        if (!AdsMenager.CheckAdAvaible()) {
            adButton.interactable = false;
        }
        GooglePlayOptions.Instance.CheckScoreAchivment(score);
        DiamondsMengerMenu.Instance.ChangeGems();
    }


    public void RewardedVideo(int diamondsDouble) {
        int currentGems = PlayerPrefs.GetInt("Gems", 0);
        currentGems = currentGems - diamondsDouble/2;
        gemsValue.text = currentGems + " + " + diamondsDouble;

        PlayerPrefs.SetInt("Gems", (diamondsDouble + currentGems));
        DiamondsMengerMenu.Instance.ChangeGems();
    }

    private void StartParticle() {

        foreach(ParticleSystem i in GetComponentsInChildren<ParticleSystem>()) {
            i.Play();
        }

    }

    public void HomeButton(string name) {
        if (AdsNormalVideo.Instance != null) {
            AdsNormalVideo.Instance.ShowStandardAd();
        }

        SceneManager.LoadScene(name);
    }

    private void OnDestroy() {
        Debug.Log("DESTROYED");
    }
}
