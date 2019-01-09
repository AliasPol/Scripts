using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerButton : MonoBehaviour {

    public Power power_type;
    public Sprite active, inactive, in_use;
    public Text power_amount;
    int powerAmount;
    Image btn_image;
    Button btn;
    bool isActive;
    bool isInUse;


    void Awake()
    {
        btn_image = GetComponent<Image>();
        btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => Click());
    }

    public void SetPower()
    {
        //powerAmount = GameData.Instance.powersAmount[GameData.Instance.lastPlayedLevel, (int)power_type];
        GetPowerCount();
        RefreshButton();
    }

    public void ResetButton()
    {

    }

    public void PowerUsed()
    {
        powerAmount--;
        RefreshButton();
        PowerUsedTimes();
    }

    private void PowerUsedTimes() {
        int used = PlayerPrefs.GetInt("Use100Powers", 0);
        used++;
        PlayerPrefs.SetInt("Use100Powers", used);

        if (used >= 100) {
            AchivmentMenager.SetAchivment(GPGSls.achievement_powerful);
        }
    }

    public void Click()
    {
        if(isActive)
        {
            if(!isInUse)
            {
                GameUI.Instance.ActivatePower(power_type);
                GameUI.Instance.currentPower = this;
                isInUse = true;
                power_amount.enabled = false;
                btn_image.sprite = in_use;
            }
            else
            {
                DeactivetePower();
            }
            Debug.LogWarning(isInUse);
        }
        else
        {
            SoundManager.Instance.PlayClip(GameUI.Instance.inactive_power);
        }
    }

    public void DeactivetePower() {
        isInUse = false;
        GameUI.Instance.DeactivatePower();
        RefreshButton();
    }

    public void FreezedPower() {
        powerAmount++;
        RefreshButton();
    }

    private void RefreshButton()
    {
        btn_image.sprite = powerAmount > 0 ? active : inactive;
        isActive = powerAmount > 0;
        UpdatePowerAmount();
    }

    void UpdatePowerAmount()
    {
        GameManager.Instance.ChangePowerValues(power_type, powerAmount);
        power_amount.enabled = powerAmount > 0 ? true : false;
        power_amount.text = powerAmount.ToString();
    }


    private void GetPowerCount() {

        switch (power_type) {
            case Power.move:
                powerAmount = GameManager.Instance.powerMove;
                break;
            case Power.freeze:
                powerAmount = GameManager.Instance.powerFreeze;
                break;
            case Power.swap:
                powerAmount = GameManager.Instance.powerSwap;
                break;
            case Power.copy:
                powerAmount = GameManager.Instance.powerCopy;
                break;
            case Power.hard:
                powerAmount = GameManager.Instance.powerHard;
                break;
            case Power.rotate:
                powerAmount = GameManager.Instance.powerRotate;
                break;
            case Power.collect:
                powerAmount = GameManager.Instance.powerCollect;
                break;
            case Power.doubled:
                powerAmount = GameManager.Instance.powerDoubled;
                break;
        }


    }
}
