using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XGDatabase : MonoBehaviour {
    //Singleton
    private static XGDatabase instance;
    public static XGDatabase Instance {
        get
        {
            if (instance == null) {
                instance = Instantiate(Resources.Load<XGDatabase>("Singletons/XGDatabase")) as XGDatabase;
                instance.name = "XGDatabase";
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    public int currentLevel;


    public bool LoadSavedPreferance(string name) {
        if (PlayerPrefs.GetInt(name, 0) == 1)
            return true;
        else
            return false;
    }

    public void SaveIntPreferance(string name, int value) {
        PlayerPrefs.SetInt(name, value);
    }

    public int GetIntPreferance(string name) {
        return PlayerPrefs.GetInt(name, 0);
    }
}
