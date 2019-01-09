using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Levels : MonoBehaviour {

    public Color gold, silver, bronze;
    public RectTransform pointer, view_content;
    public Transform levelsParent;
    ScrollRect scrollRect;
    bool center_on_level;
    float end_pos;
    float screen_width;
    float scroll_speed = 10;
    LevelSelector target;

    public float ScreenWidth
    {
        get
        {
            return screen_width;
        }
    }

    void Awake()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        Vector2 resolution = GetComponentInParent<CanvasScaler>().referenceResolution;
        screen_width = resolution.x / (resolution.y / resolution.x);
    }

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        RectTransform lastLevel = (RectTransform)levelsParent.GetChild(LevelsMenager.Instance.lastPlayedLevel-1);
        SetMapOnLevel(lastLevel);
        if (LevelsMenager.Instance.backFromGame)
            SoundManager.Instance.PlayMenuMusic();
    }

    public Color LevelColor(int levelIndex)
    {
        switch (LevelsMenager.Instance.StarsGetFromLevel(levelIndex))
        {
            case 1:
                return bronze;
            case 2:
                return silver;
            case 3:
                return gold;
            default:
                return Color.black;
        }
    }

    public void CenterOnLevel(LevelSelector selector, RectTransform selectorRect)
    {
        target = selector;
        center_on_level = true;
        SetMapPos(selectorRect);
    }

    void SetMapOnLevel(RectTransform selectorRect)
    {
        SetMapPos(selectorRect);
        Vector2 pos = new Vector2(end_pos, 0);
        view_content.anchoredPosition = pos;
    }

    void SetMapPos(RectTransform selectorRect)
    {
        end_pos = Mathf.Clamp(screen_width / 2 - selectorRect.anchoredPosition.x, -view_content.sizeDelta.x + screen_width, 0);
        pointer.anchoredPosition = selectorRect.anchoredPosition;
    }


    void Update()
    {
        if (center_on_level)
        {
            Vector2 pos = new Vector2(Mathf.Lerp(view_content.anchoredPosition.x, end_pos, scroll_speed * Time.deltaTime), 0);
            view_content.anchoredPosition = pos;
            if (Mathf.Abs(view_content.anchoredPosition.x - end_pos) < 1)
            {
                center_on_level = false;
                scrollRect.velocity = Vector2.zero;
                if(target)
                {
                    Functions.LoadLoadingScreen();
                }
            }
        }
    }

    public void BackToMenu()
    {
        //GameData.Instance.backFromMap = true;
        Functions.LoadMainMenu();
    }
}
