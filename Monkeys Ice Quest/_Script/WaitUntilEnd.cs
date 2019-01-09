using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitUntilEnd : MonoBehaviour {

    private static WaitUntilEnd instance;
    public static WaitUntilEnd Instance {
        get
        {
            if(instance == null) {
                instance = FindObjectOfType<WaitUntilEnd>();
            }
            return instance;
        }
    }

    public int timeWait;
    public Text timeCountText;
    public Text destroyedToMuchBlocksText;
    public GameObject panel;
    public PlayerController plController;



    public void WaitPanel() {
        panel.SetActive(true);
        StartCoroutine(WaitForEndGame());
    }


    private IEnumerator WaitForEndGame() {
        plController.enabled = false;
        int count = timeWait;
        for(int i=0;i<= timeWait; i++) {
            timeCountText.text = "LEVEL ENDS IN " + count;
            yield return new WaitForSeconds(1f);
            count--;
        }
        GameManager.Instance.WaitPanelEnds();
        panel.SetActive(false);

    }

    public void DestroyedToMuchBlocks() {
        destroyedToMuchBlocksText.text = "TOO MANY BLOCKS WAS DAMAGED!";
    }
}
