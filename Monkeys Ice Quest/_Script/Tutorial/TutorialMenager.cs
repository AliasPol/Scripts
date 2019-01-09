using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenager : MonoBehaviour {


    public Text textMassage;

    public GameObject[] objectToTapped;
    public GameObject[] textToRead;



    private int i;
    private int iS=0;

    public Power powerUse;
    public GameObject blockToUsePower;

    private void Start() {
        i = objectToTapped.Length-1;
    }



    public void ObjectClicked() {
        objectToTapped[i].GetComponent<IceBlockPhysics>().DestroyByTapped();
        i--;

        if (i > 0) {
            Debug.Log("FINIHED ALL BLOCKS");
        }
    }

    public void ShowText() {
        if(iS != 0) {
            HideText();
        }

        textToRead[iS].SetActive(true);
        iS++;

        if(iS >= textToRead.Length) {
            Debug.Log("Finished All Text to read");
        }
    }

    public void HideText() {
        textToRead[iS - 1].SetActive(false);
    }

    public void EndTutorial() {
        Functions.LoadGame();
    }


}
