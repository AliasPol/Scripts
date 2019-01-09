using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModsList : MonoBehaviour {

	public enum AllModsList {
        TwoBallModeList,
        DontTouchItList
    }

    public AllModsList openList;
    private GameObject currentList;

    private static ModsList instance;
    public static ModsList Instance {
        get
        {
            if(instance == null) {
                instance = FindObjectOfType<ModsList>();
            }
            return instance;
        }
    }

    public void DestroyCurrentList() {
        if(currentList != null) {
            Destroy(currentList);
        }
    }

    public void ShowList(string list) {
        DestroyCurrentList();
        currentList = Instantiate(Resources.Load("Menu/MoreMods/" + list),this.transform) as GameObject;
    }

}
