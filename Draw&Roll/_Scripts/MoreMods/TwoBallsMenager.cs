using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoBallsMenager : MonoBehaviour {

    private static TwoBallsMenager instance;
    public static TwoBallsMenager Instance {
        get
        {
            if (instance == null) {
                instance = Instantiate(Resources.Load<TwoBallsMenager>("2Balls/TwoBallsMenager")) as TwoBallsMenager;
                DontDestroyOnLoad(instance);
                instance.name = "TwoBallsMenager";
            }
            return instance;
        }
    }

    public TwoBallsList.SaveList2Ball currentLevel = TwoBallsList.SaveList2Ball.TwoBallLevel1;

    public void StartLevel() {
        Debug.Log(currentLevel + "  CURENT LEVEL");
        Instantiate(Resources.Load("2Balls/Level" + (int)currentLevel));
    }
}
