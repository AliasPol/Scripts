using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceScript : MonoBehaviour {

    public static bool updateStep = true;

    public static float distanceToEnd;
    public static string planetName;

    public Text distanceText;
    public Text planetNameText;


    private static float updatedDistance;
    private float step;

    private bool possibleToChange;


    void Update () {

        if (updateStep) {
            UpddateStep();
        }


        updatedDistance -= step * Time.deltaTime;
        distanceText.text = updatedDistance.ToString("F0");

        if(updatedDistance < 1000 && possibleToChange) {
            updateStep = false;
            RoundMenager.Instance.ChangeStage();
        }

	}

    public static float GetDistance()
    {
        return updatedDistance;
    }

    private void UpddateStep()
    {

        updatedDistance = distanceToEnd;
        //step = distanceToEnd * 180 / 10000000;
        step = 10000000 / 100;           //bylo 60
        planetNameText.text = planetName;
        updateStep = false;
        possibleToChange = true;
    }

    private void OnEnable()
    {
        updateStep = true;
    }
}
