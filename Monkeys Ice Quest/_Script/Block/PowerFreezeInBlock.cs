using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFreezeInBlock : MonoBehaviour {

    public Power freezePower;

    public void GetFrozzenPower() {

        switch (freezePower) {
            case Power.collect:
                GameManager.Instance.powerCollect++ ;
                break;
            case Power.copy:
                GameManager.Instance.powerCopy++;
                break;
            case Power.doubled:
                GameManager.Instance.powerDoubled++;
                break;
            case Power.freeze:
                GameManager.Instance.powerFreeze++;
                break;
            case Power.hard:
                GameManager.Instance.powerHard++;
                break;
            case Power.move:
                GameManager.Instance.powerMove++;
                break;
            case Power.rotate:
                GameManager.Instance.powerRotate++;
                break;
            case Power.swap:
                GameManager.Instance.powerSwap++;
                break;
        }
        FindPowerButton();
    }

    private void FindPowerButton() {
        PowerButton[] powersButton = FindObjectsOfType<PowerButton>();

        foreach(PowerButton n in powersButton) {
            Debug.Log("PEtla");
            if(freezePower == n.power_type) {
                Debug.LogWarning("ZNALAZLEM");
                n.FreezedPower();
                break;
            }

        }
    }
}
