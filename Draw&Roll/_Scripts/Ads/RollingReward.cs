using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shop;

public class RollingReward : MonoBehaviour {

    public int reward;
    public bool isDiamondReward;
    public int[] indexRewards;

    public bool isSelected;
    private bool isScaleUp = true;
    public bool isUltraRare = false;

    public void GetReward() {
        if (isDiamondReward) {
            DiamondsMengerMenu.Instance.ChangeGems(reward);
        }
        else {
            if (!isUltraRare) {
                int index = indexRewards[Random.Range(0, indexRewards.Length)];
                ShopListCreator.Instance.UnlockBall(index);
            }
            else {
                int i = PlayerPrefs.GetInt("UltraRareUnlocked", 0);

                switch (i) {
                    case 0:
                        ShopListCreator.Instance.UnlockBall(indexRewards[0]);
                        PlayerPrefs.SetInt("UltraRareUnlocked", 1);
                        break;
                    case 1:
                        ShopListCreator.Instance.UnlockBall(indexRewards[1]);
                        PlayerPrefs.SetInt("UltraRareUnlocked", 2);
                        break;
                    case 2:
                        ShopListCreator.Instance.UnlockBall(indexRewards[2]);
                        PlayerPrefs.SetInt("UltraRareUnlocked", 3);
                        break;
                    default:
                        DiamondsMengerMenu.Instance.ChangeGems(300);
                        break;
                }
            }
        }
    }

    public void SelectedToReward() {
        isSelected = !isSelected;

    }


    private void Update() {

        if (isSelected) {

            if (isScaleUp) {
                Vector3 scale = gameObject.transform.localScale;
                scale.x = scale.x + 0.01f;
                scale.y = scale.y + 0.01f;
                gameObject.transform.localScale = scale;

                if (scale.x >= 1.1f) {
                    isScaleUp = false;
                }
            }
            else {
                Vector3 scale = gameObject.transform.localScale;
                scale.x = scale.x - 0.01f;
                scale.y = scale.y - 0.01f;
                gameObject.transform.localScale = scale;

                if (scale.x <= 0.9f) {
                    isScaleUp = true;
                }
            }




        }
    }
}
