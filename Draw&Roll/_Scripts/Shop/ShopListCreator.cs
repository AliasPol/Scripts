using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Shop {
    public class ShopListCreator : MonoBehaviour {

        private int count = 113;
        public GameObject prefabs;

        private PanleShopBall currentSelectedButton;

        public static ShopListCreator Instance;

        public List<PanleShopBall> ballButtonsObject;

        private void Awake() {
            Instance = GetComponent<ShopListCreator>();
        }

        private void Start() {
            //StartCoroutine(CheckUnlocked());
        }

        public void Generate() {
            for (int i = 1; i <= count; i++) {
                GameObject ballCreated = Instantiate(prefabs, this.transform);
                PanleShopBall ball = ballCreated.GetComponent<PanleShopBall>();
                ball.index = i;
                //ball.Generate();
                ball.GenerateLocked();
                ballButtonsObject.Add(ball);
            }
        }


        private IEnumerator CheckUnlocked() {

            for(int i = 0; i < ballButtonsObject.Count; i++) {
                if(ballButtonsObject[i].unlocked == 1) {
                    ballButtonsObject[i].Generate();
                    ballButtonsObject[i].Unlocked();

                    yield return new WaitForEndOfFrame();
                }
            }
            
        }

        public void UnlockBall(int index) {
            ballButtonsObject[index].FirstTimeUnlockedBall();
        }

        public void SelectedBall(PanleShopBall button) {
            if(currentSelectedButton != null) {
                currentSelectedButton.UnSelectThisBall();
            }
            currentSelectedButton = button;
        }
    }
}