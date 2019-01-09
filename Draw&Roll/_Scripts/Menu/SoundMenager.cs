using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenager : MonoBehaviour {

    public Image icon;
    public Text text;
    public string nameButton;
    public AudioSource soundToOff;
    public Color[] color;

    public void OnClick() {

        if (soundToOff.enabled) {
            soundToOff.enabled = false;
            text.text = "PLAY " + nameButton;
            icon.color = color[1];
        }
        else{
            soundToOff.enabled = true;
            text.text = "STOP " + nameButton;
            icon.color = color[0];
        }

    }
}
