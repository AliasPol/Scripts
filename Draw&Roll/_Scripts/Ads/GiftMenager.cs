using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class GiftMenager : MonoBehaviour {

    public float msToWait = 43200000f;    //43200000f;
    public Button adButton;
    public Button diamondButton;
    public Text adTimer;
    public Transform rollContainer;
    public GameObject specialBall;

    private bool isRolling;
    private Transform[] rolls;
    private ulong timestap;
    private float transition;
    private RollingReward giftReward;



    public static GiftMenager Instance;

    private void Awake() {

        Instance = GetComponent<GiftMenager>();

        rolls = new Transform[rollContainer.childCount];
        for(int i = 0; i< rollContainer.childCount; i++) {
            rolls[i] = rollContainer.GetChild(i);
        }

        timestap = ulong.Parse(PlayerPrefs.GetString("LastChestReward", "0"));
        if (!IsChestReady()) {
            adButton.interactable = false;
        }
        specialBall = Instantiate(specialBall, rolls[1].transform.position, rolls[1].transform.rotation ,rollContainer) as GameObject;
    }

    private void Update() {


        if (!adButton.interactable ) {
            if (IsChestReady() && !isRolling) {
                adButton.interactable = true;
                string s = "GET FREE ROLL";
                adTimer.text = s;
                return;
            }

            if (!Advertisement.IsReady() && !isRolling) {
                adButton.interactable = false;
                string s = "CHECK YOUR CONNECTION";
                adTimer.text = s;
            }
        }

        if (isRolling) {
            Vector3 end = (-Vector3.right * 401) * (rolls.Length);            
            rollContainer.transform.localPosition = Vector3.Lerp(Vector3.right * 401, end, transition);
            transition += Time.deltaTime / 3f;

            if (transition > 1) {
                giftReward.SelectedToReward();
                isRolling = false;
                diamondButton.interactable = true;
                giftReward.GetReward();
            }
        }
        
    }

    public void GetFreeRoll() {

        if (IsChestReady()) {
            timestap = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastChestReward", timestap + "");
            adButton.interactable = false;
            Roll();

            int roll = XGDatabase.Instance.GetIntPreferance("Roll");
            roll++;
            XGDatabase.Instance.SaveIntPreferance("Roll", roll);
            GooglePlayOptions.Instance.CheckRollAchievment(roll);
        }

    }

    public void GetCostRoll(int cost) {
            
        if(DiamondsMengerMenu.Instance.currentGems + cost >= 0) {
            DiamondsMengerMenu.Instance.ChangeGems(cost);
            Roll();
        }
    }

    private void Roll() {
        int chance = UnityEngine.Random.Range(0, 101);
        

        if (giftReward != null) 
            giftReward.SelectedToReward();


        diamondButton.interactable = false;
        adButton.interactable = false;

        transition = 0;
        isRolling = true;
        float offset = 0.0f;
        List<int> indexes = new List<int>();

        for(int i= 0; i< rolls.Length; i++) {
            indexes.Add(i);
        }

        int index=0;
        int specialBallIndex = UnityEngine.Random.Range(0, indexes.Count - 1);
        for (int i = 0; i < rolls.Length; i++) {
            index = indexes[UnityEngine.Random.Range(0, indexes.Count)];
            indexes.Remove(index);
            rolls[index].transform.localPosition = Vector3.right * offset;
            offset += 400;

            if (specialBallIndex == i && chance < 99 && specialBall != null) {
                specialBall.transform.localPosition = Vector3.right * offset;
                offset += 400;
            }
        }

        if (chance > 98 && specialBall != null) {
            specialBall.transform.localPosition = Vector3.right * offset;
            offset += 400;
            giftReward = specialBall.GetComponent<RollingReward>();
        }
        else {
            giftReward = rolls[index].GetComponent<RollingReward>();
        }

    }

    public void HideReward() {
        rollContainer.transform.localPosition = Vector3.right * 400f;
    }


    private bool IsChestReady() {

        ulong diff = (ulong)DateTime.Now.Ticks - timestap;
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000f;

        if (secondsLeft < 0) {
            
            return true;
        }

        TimeLeft(secondsLeft);

        return false;

    }

    private void TimeLeft(float timeLeftS) {
        string r = "";
        r+= ((int)timeLeftS/3600).ToString() + "H ";

        timeLeftS -= ((int)timeLeftS / 3600) * 3600;
        r+= ((int)timeLeftS / 60).ToString("00") + "M ";

        r += (timeLeftS % 60).ToString("00") + "S";

        adTimer.text = r;

    }
}
