using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Shop {

    public class PanleShopBall : MonoBehaviour {

        public int index=1;
        public Text costText;
        public Image lockedImage;
        public GameObject ball;
        public int unlocked;
        private int cost;
        private Button thisButton;

        private void Awake() {
            thisButton = GetComponent<Button>();
        }
        
        private void Start() {

            if (index != 1)
                unlocked = PlayerPrefs.GetInt("PlayerBall" + index, 0);
            else
                unlocked = 1;

            if (unlocked==1) {
                Generate();
                Unlocked();
            }
            else {
                GenerateLocked();
            }
            
        }

        public void Generate() {

                ball = Instantiate(Resources.Load<GameObject>("Player/PlayerBall" + index)) as GameObject;
                ball.transform.SetParent(gameObject.transform);
                ball.transform.position = this.transform.position;
                Destroy(ball.GetComponent<Rigidbody2D>());
                Destroy(ball.GetComponent<CircleCollider2D>());
                foreach (ParticleSystem a in ball.GetComponentsInChildren<ParticleSystem>()) {
                    
                    ParticleSystemRenderer b = a.GetComponent<ParticleSystemRenderer>();
                if (b != null) {
                    b.sortingLayerName = "UI";
                    b.sortingOrder = 1;
                }
                }
            Locked();
            
        }

        public void Unlocked() {

            ball.SetActive(true);
            costText.text = "UNLOCKED";
            lockedImage.enabled = false;
            if ("PlayerBall"+index == PlayerPrefs.GetString("SelectedPlayer", "PlayerBall1"))
                SetThisBall();
        }

        private void Locked() {
            ball.SetActive(false);
            lockedImage.enabled = true;
            SetCost();
        }

        public void GenerateLocked() {
            lockedImage.enabled = true;
            SetCost();
        }

        public void ClickedToBuy() {
            if(unlocked == 1) {
                SetThisBall();
            }
            else {
                if (TakeCost()) {
                    FirstTimeUnlockedBall();
                }
            }
        }

        public void FirstTimeUnlockedBall() {
            Generate();
            lockedImage.enabled = false;
            Unlocked();
            SetThisBall();
            unlocked = 1;
            PlayerPrefs.SetInt("PlayerBall" + index, 1);
        }

        private void SetThisBall() {
            ShopListCreator.Instance.SelectedBall(this);
            PlayerPrefs.SetString("SelectedPlayer", "PlayerBall" + index);
            costText.text = "SELECTED";
            thisButton.interactable = false;
        }

        public void UnSelectThisBall() {
            costText.text = "UNLOCKED";
            thisButton.interactable = true;
        }

        

        private bool TakeCost() {
            if(cost <= DiamondsMengerMenu.Instance.currentGems) {
                DiamondsMengerMenu.Instance.ChangeGems(-cost);
                return true;
            }
            else {
                Debug.Log("TO LOW INFLUENCE");
                return false;
            }
        }

        


        private void SetCost() {
            if (index < 15) {
                costText.text = 250 + "";
                cost = 250;
            }
            else if (index < 30) {
                costText.text = 500 + "";
                cost = 500;
            }
            else if (index < 45) {
                costText.text = 1000 + "";
                cost = 1200;
            }
            else if (index < 60) {
                costText.text = 1500 + "";
                cost = 1500;
            }
            else if (index < 75) {
                costText.text = 2000 + "";
                cost = 2000;
            }
            else if (index < 90) {
                costText.text = 2500 + "";
                cost = 2500;
            }
            else if(index < 105) {
                costText.text = 3000 + "";
                cost = 3000;
            }
            else if(index > 900) {
                costText.text = "GIFT";
                cost = 5000000;
            }
            else {
                costText.text = 3500 + "";
                cost = 3500;
            }
        }
    }
}