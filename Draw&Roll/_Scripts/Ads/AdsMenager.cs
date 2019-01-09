using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsMenager : MonoBehaviour {

    public static float msToWait = 300000f;

    


    public void ShowAd() {

        if (Advertisement.IsReady()) {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }

    public static bool CheckAdAvaible() {
        return Advertisement.IsReady();
    }

    

    private void HandleAdResult(ShowResult result) {

        switch (result) {
            case ShowResult.Finished:
                if (GameMenager.Instance != null) {
                    GameMenager.Instance.DubleDiamonds();
                }
                else {
                    GiftMenager.Instance.GetFreeRoll();
                }

                Debug.Log("PLAYER FINISHED AD");
                break;
            case ShowResult.Skipped:
                Debug.Log("Player SKIPPED OUR AD");
                break;
            case ShowResult.Failed:
                Debug.Log("SHOWING AD FAILED");
                break;
        }

    }


    public static void CheckAdd() {

        ulong timestap = ulong.Parse(PlayerPrefs.GetString("LastChestReward", "0"));
        ulong diff = (ulong)DateTime.Now.Ticks - timestap;
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000f;


    }


}
