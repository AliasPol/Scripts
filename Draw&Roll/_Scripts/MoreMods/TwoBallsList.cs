using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Menu;

public class TwoBallsList : MonoBehaviour {

    public enum SaveList2Ball {
        TwoBallLevel1,
        TwoBallLevel2,
        TwoBallLevel3,
        TwoBallLevel4,
        TwoBallLevel5,
        TwoBallLevel6,
        TwoBallLevel7,
        TwoBallLevel8,
        TwoBallLevel9,
        TwoBallLevel10,
        TwoBallLevel11,
        TwoBallLevel12,
        TwoBallLevel13,
        TwoBallLevel14,
        TwoBallLevel15,
        TwoBallLevel16,
        TwoBallLevel17,
        TwoBallLevel18,
        TwoBallLevel19,
        TwoBallLevel20,
        TwoBallLevel21,
        TwoBallLevel22,
        TwoBallLevel23,
        TwoBallLevel24,
        TwoBallLevel25,
        TwoBallLevel26,
        TwoBallLevel27,
        TwoBallLevel28,
        TwoBallLevel29,
        TwoBallLevel30,
        TwoBallLevel31,
        TwoBallLevel32,
        TwoBallLevel33,
        TwoBallLevel34,
        TwoBallLevel35,
        TwoBallLevel36,
        TwoBallLevel37,
        TwoBallLevel38,
        TwoBallLevel39,
        TwoBallLevel40
    };

    public SaveList2Ball selectedLevel;
    public Text textLevel;
    public Image lockImage;


    private Button bt;


    private void Awake() {
        bt = GetComponent<Button>();
        CheckButtonUnlocked();
    }

    private void CheckButtonUnlocked() {
        if (selectedLevel != SaveList2Ball.TwoBallLevel1) {
            
            if (XGDatabase.Instance.LoadSavedPreferance(selectedLevel.ToString()))
                Unlock();
            else
                Lock();
          
        }
        else
            Unlock();
    }

    public void Unlock() {
        Destroy(lockImage.gameObject);
        bt.interactable = true;
        bt.onClick.AddListener(() => Click());
        bt.onClick.AddListener(() => MenuMenager.Instance.CloseCurrentPopupMenu());
    }

    public void Lock() {
        textLevel.gameObject.SetActive(false);
        bt.interactable = false;
    }

    public void Click() {
        TwoBallsMenager.Instance.currentLevel = selectedLevel;
        FadeMenu.Instance.FadeMenuShow("2Balls");
        ModsList.Instance.DestroyCurrentList();
    }

}
