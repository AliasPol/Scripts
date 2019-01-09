using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System;

public class MenagerAudio : MonoBehaviour {

    private static MenagerAudio instance;
    public static MenagerAudio Instance
    {
        get
        {
            if (instance == null) {
                GameObject temp = Instantiate(Resources.Load("AudioMenager")) as GameObject;
                temp.name = "AudioMenager";
                instance = temp.GetComponent<MenagerAudio>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }


    public AudioMixer _audioMixer;

    public List<GroupsAudio> allMixerGroups;

    private void Awake()
    {
        allMixerGroups = new List<GroupsAudio>();

        foreach(AudioMixerGroup z in _audioMixer.FindMatchingGroups("Master")) {

            GroupsAudio gA = new GroupsAudio();

            gA.name = z.name;
            gA.mixerGroup = z;
            gA.audioSource = gameObject.AddComponent<AudioSource>();
            gA.audioSource.outputAudioMixerGroup = z;
            gA.enumName = Sound.GetMixGroupsName(z.name);
            gA.volume = 1;
            allMixerGroups.Add(gA);
        }
    }

    public void PlaySoundEffect(Sound.EffectList effectSound)
    {
        GroupsAudio gA = null;
        foreach(GroupsAudio z in allMixerGroups) {
            if(z.enumName == Sound.MixGroupsName.Effects) {
                
                gA = z;
                break;
            }
        }

        AudioClip aC = Resources.Load("Sound/Effects/" + effectSound) as AudioClip;

        gA.audioSource.clip = aC;
        gA.audioSource.Play();
    }

    public void PlayPlayerSoundEffect(Sound.PlayerEffectList effectSound)
    {
        GroupsAudio gA = null;
        foreach (GroupsAudio z in allMixerGroups) {
            if (z.enumName == Sound.MixGroupsName.PlayerEffects) {
                gA = z;
                break;
            }
        }

        AudioClip aC = Resources.Load("Sound/PlayerEffect/" + effectSound) as AudioClip;
        gA.audioSource.clip = aC;
        gA.audioSource.Play();
    }

    public void PlayPlayerSoundEffectLoop(Sound.PlayerEffectLoopList effectSound)
    {
        GroupsAudio gA = null;
        foreach (GroupsAudio z in allMixerGroups) {
            if (z.enumName == Sound.MixGroupsName.PlayerEffectsLoop) {
                gA = z;
                break;
            }
        }

        AudioClip aC = Resources.Load("Sound/PlayerEffect/" + effectSound) as AudioClip;

        gA.audioSource.clip = aC;
        gA.audioSource.loop = true;
        gA.audioSource.Play();
    }

    public void PlayMusic(Sound.MusicList musicName)
    {
        GroupsAudio gA = null;
        foreach (GroupsAudio z in allMixerGroups) {
            if (z.enumName == Sound.MixGroupsName.Music) {
                gA = z;
                break;
            }
        }

        AudioClip aC = Resources.Load("Sound/Music/" + musicName) as AudioClip;

        gA.audioSource.clip = aC;
        gA.audioSource.loop = true;
        gA.audioSource.Play();
    }

    public void TurnOffVolume(Sound.MixGroupsName groupsName)
    {
        GroupsAudio gA = null;
        foreach (GroupsAudio z in allMixerGroups) {
            if (z.enumName == groupsName) {
                gA = z;
                break;
            }
        }

        gA.audioSource.volume = 0f;
    }

    public void TurnOnVolume(Sound.MixGroupsName groupsName)
    {
        GroupsAudio gA = null;
        foreach (GroupsAudio z in allMixerGroups) {
            if (z.enumName == groupsName) {
                gA = z;
                break;
            }
        }

        gA.audioSource.volume = gA.volume;
    }

    public void StopAllSound()
    {
        foreach(GroupsAudio z in allMixerGroups) {
            z.audioSource.Stop();
        }
    }

}
