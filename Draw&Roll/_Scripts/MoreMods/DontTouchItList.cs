using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Menu;

public class DontTouchItList : MonoBehaviour {

    public enum SaveList {
        DontTouchItLevel1,
        DontTouchItLevel2,
        DontTouchItLevel3,
        DontTouchItLevel4,
        DontTouchItLevel5,
        DontTouchItLevel6,
        DontTouchItLevel7,
        DontTouchItLevel8,
        DontTouchItLevel9,
        DontTouchItLevel10,
        DontTouchItLevel11,
        DontTouchItLevel12,
        DontTouchItLevel13,
        DontTouchItLevel14,
        DontTouchItLevel15,
        DontTouchItLevel16,
        DontTouchItLevel17,
        DontTouchItLevel18,
        DontTouchItLevel19,
        DontTouchItLevel20,
        DontTouchItLevel21,
        DontTouchItLevel22,
        DontTouchItLevel23,
        DontTouchItLevel24,
        DontTouchItLevel25,
        DontTouchItLevel26,
        DontTouchItLevel27,
        DontTouchItLevel28,
        DontTouchItLevel29,
        DontTouchItLevel30,
        DontTouchItLevel31,
        DontTouchItLevel32,
        DontTouchItLevel33,
        DontTouchItLevel34,
        DontTouchItLevel35,
        DontTouchItLevel36,
        DontTouchItLevel37,
        DontTouchItLevel38,
        DontTouchItLevel39,
        DontTouchItLevel40
    };

    public SaveList selectedLevel;
    public Text textLevel;
    public Image lockImage;


    private Button bt;


    private void Awake() {
        bt = GetComponent<Button>();
        CheckButtonUnlocked();
    }

    private void CheckButtonUnlocked() {
        if (selectedLevel != SaveList.DontTouchItLevel1) {

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
        DontTouchItMenager.Instance.currentLevel = selectedLevel;
        FadeMenu.Instance.FadeMenuShow("DontTouchIt");
        ModsList.Instance.DestroyCurrentList();
    }



}
