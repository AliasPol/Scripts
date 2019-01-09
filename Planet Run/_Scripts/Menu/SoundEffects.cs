using UnityEngine;
using UnityEngine.UI;

public class SoundEffects : MonoBehaviour {

    public Sound.EffectList playEffect = Sound.EffectList.UIButtonV1;

    public void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlayEffect);
    }

    public void PlayEffect()
    {
        MenagerAudio.Instance.PlaySoundEffect(playEffect);
    }
}
