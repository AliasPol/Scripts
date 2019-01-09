using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineScript : MonoBehaviour {

    public bool jump;
    public bool turnOff;
    public bool jetStart;
	
	void Start () {

        if (jump) {
            MenagerAudio.Instance.StopAllSound();
            MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.AlienJumpv2);
        }

        if (turnOff)
            MenagerAudio.Instance.StopAllSound();

        if (jetStart)
            MenagerAudio.Instance.PlayPlayerSoundEffect(Sound.PlayerEffectList.JetApproachingPlanetSequence);
	}
	
	
}
