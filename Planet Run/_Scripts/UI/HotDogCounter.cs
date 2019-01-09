using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotDogCounter : MonoBehaviour {

    private static int hotDogCounter = 0;

    private static Text textCounterView;

    public static int HotDogValue
    {
        get
        {
            return hotDogCounter;
        }
        set
        {
            hotDogCounter = value;
            textCounterView.text = hotDogCounter.ToString("F0");
        }
    }

    private void Awake()
    {
        textCounterView = GetComponentInChildren<Text>();
    }

}
