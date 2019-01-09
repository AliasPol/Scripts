using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public AudioClip standardButton, levelActive, levelInactive;
    public AudioClip menu_music;
    public AudioClip[] game_music;
    List<AudioSource> soundsSources = new List<AudioSource>();

    public AudioSource music;
    AudioSource sounds;
    bool soundsMuted, musicMuted;
    float volume;
    float playingTime = 0;
    float clipTime;
    bool gameMusic;


    private static SoundManager _Instance;
    public static SoundManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<SoundManager>();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        sounds = GetComponent<AudioSource>();
        
    }

    void Start()
    {
        volume = 0.4f;
        soundsMuted = AEDatabase.HasKey("soundsMuted") ? AEDatabase.GetBool("soundsMuted") : false;
        musicMuted = AEDatabase.HasKey("musicMuted") ? AEDatabase.GetBool("musicMuted") : false;
        
        sounds.volume = volume;
        sounds.mute = soundsMuted;
        music.volume = volume;
        music.mute = musicMuted;
    }

    public void SoundsMuted(bool mute)
    {
        sounds.mute = mute;
        soundsMuted = mute;
    }

    public void MusicMuted(bool mute)
    {
        music.mute = mute;
        musicMuted = mute;
    }

    public void PlayMenuMusic()
    {
        gameMusic = false;
        music.loop = true;
        music.clip = menu_music;
        music.Play();
    }

    public void PlayGameMusic()
    {
        music.loop = false;
        gameMusic = true;
    }

    void Update()
    {
        if(gameMusic)
        {
            if(playingTime >= clipTime)
            {
                PlayNewGameClip();
                
            }
            playingTime += Time.deltaTime;
        }
    }

    void PlayNewGameClip()
    {
        AudioClip clip = game_music[Random.Range(0, game_music.Length - 1)];
        clipTime = clip.length;
        playingTime = 0;
        music.clip = clip;
        music.Play();
    }

    public void PlayClip(Sounds clip)
    {
        if (soundsSources.Count > 0)
        {
            AudioSource soundSource = GetFreeAudioSource();
            if (soundSource)
                StartCoroutine(PlayClipSound(clip, soundSource));
            else 
            {
                StartCoroutine(PlayClipSound(clip));
            }
        }
        else
        {
            StartCoroutine(PlayClipSound(clip));
        }

    }

    public void PlayClip(AudioClip clip)
    {
        if (soundsSources.Count > 0)
        {
            AudioSource soundSource = GetFreeAudioSource();
            if (soundSource)
                StartCoroutine(PlayClipSound(clip, soundSource));
            else
            {
                StartCoroutine(PlayClipSound(clip));
            }
        }
        else
        {
            StartCoroutine(PlayClipSound(clip));
        }

    }

    IEnumerator PlayClipSound(Sounds clip, AudioSource _source = null)
    {
        AudioSource source;
        if (_source == null)
        {
            GameObject tmpAudio = new GameObject("TmpAudio");
            tmpAudio.transform.SetParent(transform);
            tmpAudio.transform.localPosition = Vector3.zero;
            source = tmpAudio.AddComponent<AudioSource>();
            soundsSources.Add(source);
        }
        else
        {
            source = _source;
        }
        source.clip = Clip(clip);
        source.mute = soundsMuted;
        source.volume = sounds.volume;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.clip = null;
    }

    IEnumerator PlayClipSound(AudioClip clip, AudioSource _source = null)
    {
        AudioSource source;
        if (_source == null)
        {
            GameObject tmpAudio = new GameObject("TmpAudio");
            tmpAudio.transform.SetParent(transform);
            tmpAudio.transform.localPosition = Vector3.zero;
            source = tmpAudio.AddComponent<AudioSource>();
            soundsSources.Add(source);
        }
        else
        {
            source = _source;
        }
        source.clip = clip;
        source.mute = soundsMuted;
        source.volume = sounds.volume;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.clip = null;
    }

    AudioSource GetFreeAudioSource()
    {
        for(int i = 0; i < soundsSources.Count; i++)
        {
            if(soundsSources[i].clip == null)
            {
                return soundsSources[i];
            }
        }
        return null;
    }

    public AudioClip Clip(Sounds sound)
    {
        AudioClip clip = null;
        switch (sound)
        {
            case Sounds.standardButton:
                clip = standardButton;
                break;
            case Sounds.levelActive:
                clip = levelActive;
                break;
            case Sounds.levelInactive:
                clip = levelInactive;
                break;
        }

        return clip;
    }
}
