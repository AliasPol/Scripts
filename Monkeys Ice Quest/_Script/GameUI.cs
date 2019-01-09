using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    public EndWindow end_window;
    public GameObject pause_window;
    public PowerButton[] power_buttons;
    public AudioClip active_power, inactive_power;
    public Text moves_to_make, collected_fruits, possible_mistakes;

    public PowerButton currentPower;



    private static GameUI _Instance;
    public static GameUI Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<GameUI>();
            }
            return _Instance;
        }
    }



    void Start()
    {
        SoundManager.Instance.PlayGameMusic();
        pause_window.SetActive(false);
        ResetPowerButtons();

    }

    public void Pause()
    {
        pause_window.SetActive(true);
    }

    public void EndScreen(bool value) {
        
        end_window.ShowWindow(value);

        if(value && LevelsMenager.Instance.indexCurrentLevel == 1) {
            AchivmentMenager.SetAchivment(GPGSls.achievement_nice_to_see_you);
            PlayerPrefs.SetInt("PassFirstLevel", 1);
        }
    }

    void ResetPowerButtons()
    {
        for(int i = 0; i < power_buttons.Length; i++)
        {
            power_buttons[i].SetPower();
        }
    }

    public void ActivatePower(Power usedPower)
    {
        if (currentPower != null) {
            currentPower.DeactivetePower();
        }
        
        GameManager.Instance.currentPower = usedPower;
        SoundManager.Instance.PlayClip(active_power);
    }

    public void DeactivatePower()
    {
        BlockBehavior.Instance.UncheckPower();
        GameManager.Instance.currentPower = Power.none;
        SoundManager.Instance.PlayClip(active_power);
    }

    public void UsedPower(bool used) {
        if(used)
            currentPower.PowerUsed();

        currentPower.DeactivetePower();
    }
}
