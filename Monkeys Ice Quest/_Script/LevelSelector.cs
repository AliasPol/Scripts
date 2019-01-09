using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour {

    public Text level_index_label;
    int level_index;
    Levels levels;
    RectTransform rectTrans;
    Button btn;

    void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        levels = GetComponentInParent<Levels>();
        level_index = transform.GetSiblingIndex();
        level_index++;
        btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => Click());
    }

    void Start()
    {
        level_index_label.text = (level_index).ToString();
        level_index_label.enabled = level_index <= LevelsMenager.Instance.unlockedLevels;
        level_index_label.color = levels.LevelColor(level_index);
        if (level_index == LevelsMenager.Instance.lastPlayedLevel)
            levels.pointer.anchoredPosition = rectTrans.anchoredPosition;
    }

    public void Click()
    {
        if(btn.interactable)
        {
            if (level_index <= LevelsMenager.Instance.unlockedLevels)
            {
                btn.interactable = false;
                SoundManager.Instance.PlayClip(Sounds.levelActive);
                if (level_index != LevelsMenager.Instance.lastPlayedLevel)
                {
                    LevelsMenager.Instance.lastPlayedLevel = level_index;
                    levels.CenterOnLevel(this, rectTrans);
                    LevelsMenager.Instance.sceneName = "Game";
                    LevelsMenager.Instance.indexCurrentLevel = level_index;
                }
                else
                {
                    LevelsMenager.Instance.sceneName = "Game";
                    LevelsMenager.Instance.indexCurrentLevel = level_index;
                    Functions.LoadLoadingScreen();
                }
            }
            else
                SoundManager.Instance.PlayClip(Sounds.levelInactive);
        }
        
    }
}
