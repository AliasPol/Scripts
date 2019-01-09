using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondsMengerMenu : MonoBehaviour {

    public Text gemsValueText;
    public static DiamondsMengerMenu Instance;
    public int currentGems;

    private void Awake() {
        Instance = gameObject.GetComponent<DiamondsMengerMenu>();
        currentGems = PlayerPrefs.GetInt("Gems", 0);
        gemsValueText.text = currentGems + "";
    }


    public void ChangeGems() {
        currentGems = PlayerPrefs.GetInt("Gems", 0);
        gemsValueText.text = currentGems + "";

        
    }

    public void ChangeGems(int value) {
        currentGems += value;
        PlayerPrefs.SetInt("Gems", currentGems);
        gemsValueText.text = currentGems + "";

        int allCollect = PlayerPrefs.GetInt("AllGems", 0);
        allCollect += value;
        PlayerPrefs.SetInt("AllGems", allCollect);

        CheckGemsAchivment(allCollect);
        GooglePlayOptions.Instance.SetLeaderboardDiamond(allCollect);
    }


    public void CheckGemsAchivment(int gems) {

        switch (gems) {
            case 500:
                GooglePlayOptions.Instance.SetAchivment(LineRiderGoogle.achievement_it_shines);
                break;
            case 1000:
                GooglePlayOptions.Instance.SetAchivment(LineRiderGoogle.achievement_im_rich);
                break;
            case 2000:
                GooglePlayOptions.Instance.SetAchivment(LineRiderGoogle.achievement_im_the_best);
                break;
            default:

                break;
        }
    }
}
