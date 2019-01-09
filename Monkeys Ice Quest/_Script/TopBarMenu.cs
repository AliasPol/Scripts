using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarMenu : MonoBehaviour {

    public Text moveValue;
    public Text fruitValue;
    public Text mistakesValue;

    private static TopBarMenu _Instance;
    public static TopBarMenu Instance {
        get
        {
            if (_Instance == null) {
                _Instance = FindObjectOfType<TopBarMenu>();
            }
            return _Instance;
        }
    }

    public string MoveValue {
        set { moveValue.text = value; }
        get { return moveValue.text; }
    }
    public string FruitValue {
        set { fruitValue.text = value; }
        get { return fruitValue.text; }
    }
    public string MistakesValue {
        set { mistakesValue.text = value; }
        get { return mistakesValue.text; }
    }


}
