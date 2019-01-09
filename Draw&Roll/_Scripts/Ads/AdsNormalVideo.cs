using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsNormalVideo : MonoBehaviour {

    private static AdsNormalVideo instance;
    public static AdsNormalVideo Instance {
        get
        {
            if (instance == null) {
                instance = FindObjectOfType<AdsNormalVideo>();
            }
            return instance;
        }
    }

    public float timeWait = 5f;


    public void ShowStandardAd() {

        float i = Time.realtimeSinceStartup;
        i = i / 60;


        if (i > timeWait && Advertisement.IsReady()) {
            Advertisement.Show("video", new ShowOptions() { resultCallback = NormalVideo });
            Time.timeScale = 0f;
            timeWait = i;
            timeWait += 15f;
        }

    }

    private void NormalVideo(ShowResult result) {

        switch (result) {
            case ShowResult.Finished:
                DiamondsMengerMenu.Instance.ChangeGems(10);
                Debug.Log("PLAYER FINISHED AD");
                break;
            case ShowResult.Skipped:
                Debug.Log("Player SKIPPED OUR AD");
                break;
            case ShowResult.Failed:
                Debug.Log("SHOWING AD FAILED");
                break;
        }

        Time.timeScale = 1f;
    }
}
