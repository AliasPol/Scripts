using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EarthMenager : MonoBehaviour {

    private static EarthMenager instance;
    public static EarthMenager Instance
    {
        get
        {
            if(instance == null) {
                instance = FindObjectOfType<EarthMenager>();
            }
            return instance;
        }
    }

    private static int index = 0;

    private float timePassed = 0;
    private float timeStep = 5144891.8f;
    private float timeCheck = 5144891.8f;

    public PlanetsInfoData[] earthInfo;

    public static int currentEarthIndex = 0;

    private void Start()
    {
        timeCheck = DistanceScript.distanceToEnd - timeStep;
        
    }

    private void Update()
    {
        timePassed = DistanceScript.GetDistance();
        if (timePassed < timeCheck && index < 5) {
            Debug.Log("Change");
            SetIndex();
            RoundMenager.Instance.UpdateEarthInfo();
            timeCheck -= timeStep;
            FindObjectOfType<SpawnSystem>().UpdateSet();


            TeleportAlien();
        }
    }

    public GameObject GetPlanetLandObstacle()
    {
        return earthInfo[currentEarthIndex].SelectObstacleLand();
    }
    public GameObject GetLandStart()
    {
        return earthInfo[currentEarthIndex].obstacleStart;
    }
    public GameObject[] GetAllLandSet()
    {
        return earthInfo[currentEarthIndex].obstacleSet;
    }
    public Material GetCurrentMaterial()
    {
        return earthInfo[currentEarthIndex].materialPlanet;
    }
    public PlanetsInfoData GetInfoOfCurrentStage()
    {
        return earthInfo[currentEarthIndex];
    }


    public void SetIndex()
    {
        index++;
        switch (index) {
            case 1:
                currentEarthIndex = 3;
                break;
            case 2:
                currentEarthIndex = 0;
                break;
            case 3:
                currentEarthIndex = 1;
                break;
            case 4:
                currentEarthIndex = 0;
                break;
            case 5:
                currentEarthIndex = 2;
                break;
        }
    }

    private void TeleportAlien()
    {
        GameObject temp = Instantiate(Resources.Load("AlienTeleport")) as GameObject;
    }
}
