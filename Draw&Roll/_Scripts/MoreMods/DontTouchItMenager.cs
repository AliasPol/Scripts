using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontTouchItMenager : MonoBehaviour {

    private static DontTouchItMenager instance;
    public static DontTouchItMenager Instance {
        get
        {
            if (instance == null) {
                instance = Instantiate(Resources.Load<DontTouchItMenager>("DontTouchIt/DontTouchItMenager")) as DontTouchItMenager;
                DontDestroyOnLoad(instance);
                instance.name = "DontTouchItMenager";
            }
            return instance;
        }
    }

    public DontTouchItList.SaveList currentLevel = DontTouchItList.SaveList.DontTouchItLevel1;

    public void StartLevel() {
        Debug.Log(currentLevel + "  CURENT LEVEL");
        Instantiate(Resources.Load("DontTouchIt/Level" + (int)currentLevel));
    }
}
