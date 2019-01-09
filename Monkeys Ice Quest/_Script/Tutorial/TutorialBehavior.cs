using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehavior : MonoBehaviour {

    public TutorialMenager tMenager;
    public GameObject click;
    public GameObject buttonEnd;

    public GameObject blockUsedPower;


    public void ClickOnObject() {
        click.SetActive(true);
        StartCoroutine(WaitAndDisable());
        tMenager.ObjectClicked();
    }

    public void ChangeText() {
        tMenager.ShowText();
    }

    private IEnumerator WaitAndDisable() {
        yield return new WaitForSeconds(0.3f);
        click.SetActive(false);
    }

    public void EndTutorial() {
        tMenager.HideText();
        buttonEnd.SetActive(true);
    }

    public void ClickPower() {
        click.SetActive(true);
        StartCoroutine(WaitAndDisable());

        PowerButton[] allPowers = FindObjectsOfType<PowerButton>();
        foreach(PowerButton n in allPowers) {
            if(n.power_type == tMenager.powerUse) {
                n.Click();
                break;
            }
        }


    }

    public void UsePower() {
        blockUsedPower.SetActive(true);
    }

    public void SwitchPower(int i) {
        BlockBehavior.Instance.StartScript(tMenager.powerUse, tMenager.objectToTapped[i]);
    }


    public void StopPower() {
        tMenager.blockToUsePower.transform.position = blockUsedPower.transform.position;
        blockUsedPower.SetActive(false);
    }

    
}
