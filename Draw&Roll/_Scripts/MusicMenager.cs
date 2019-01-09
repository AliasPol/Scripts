using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenager : MonoBehaviour {

    public AudioClip[] sound;
    public float[] soundVolume;
    public AudioSource aSource;

    private void Awake() {
        int index = PlayerPrefs.GetInt("SoundClip", 0);
        aSource.clip = sound[index];
        aSource.volume = soundVolume[index];
        aSource.Play();
    }

    public void ChangeClip() {
        int index = PlayerPrefs.GetInt("SoundClip", 0);
        index++;

        if (index >= sound.Length)
            index = 0;

        aSource.clip = sound[index];
        aSource.volume = soundVolume[index];
        aSource.Play();

        PlayerPrefs.SetInt("SoundClip", index);
    }
}
