using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSlider : MonoBehaviour {

    public Slider slider;
    private float fuelValue = 100f;

    private float timeSpawn = 45f;

    private void Update()
    {
        float step = 10 / 9 * Time.deltaTime;
        fuelValue -= step;
        slider.value = fuelValue;
        timeSpawn -= Time.deltaTime;

        if(timeSpawn <= 0) {
            timeSpawn = 45f;
            SpawnSystem.spawnFuel = true;
        }
    }

    public void Refile()
    {
        fuelValue = 100;
        slider.value = fuelValue;
    }
}
