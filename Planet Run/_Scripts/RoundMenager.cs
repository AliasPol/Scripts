using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundMenager : MonoBehaviour {

    private static RoundMenager instance;
    public static RoundMenager Instance
    {
        get
        {
            if(instance == null) {
                instance = FindObjectOfType<RoundMenager>();
            }
            return instance;
        }
    }

    private int currentPlanetsIndex = 0;

    public bool space = true;

    public PlanetsInfo planetsInfo;

    public GameObject shootPrefab;
    public GameObject fuealPrefab;

    private void Awake()
    {
        if (FindObjectsOfType<RoundMenager>().Length == 1) {
            DontDestroyOnLoad(this);

          //  GameObject ob = Instantiate(Resources.Load("-------UI DontDestroy")) as GameObject;
          //  DontDestroyOnLoad(ob);

            SetUI();
        }
        else {
            Destroy(gameObject);
        }

        
        
    }



    public void ChangeStage()
    {
        Fade();
        
        if (space) {
            TransitionSpaceToLand();
           // ChangeStageToLand();
        }
        else {
            TransitionLandToSpace();
            //ChangeStageToSpace();
        }
    }
    public void TransitionSpaceToLand()
    {
        space = false;
        SetUI();
        SceneManager.LoadScene(6);
    }

    public void TransitionLandToSpace()
    {
        space = true;
        SceneManager.LoadScene(5);
    }

    public void ChangeStage(int index)
    {
        currentPlanetsIndex = index;
        PlanetsInfoData plantesData = Resources.Load("PlanetsInfo/" + planetsInfo.planetsNames[currentPlanetsIndex]) as PlanetsInfoData;
        SetPlanetsData(plantesData);
        SetUI();
        SceneManager.LoadScene(1);
        Debug.Log(currentPlanetsIndex);
    }

    public void ChangeStageToLand()
    {
        space = false;
        SetUI();
        if (currentPlanetsIndex != 2)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(4);
        
    }

    public void ChangeStageToSpace()
    {
       /* if (currentPlanetsIndex == 2 && EarthMenager.currentEarthIndex == -1) {
            EarthMenager.currentEarthIndex = 0;
            Debug.Log("TU JESTEM");
            SceneManager.LoadScene(4);
        }
        else {*/
            currentPlanetsIndex++;
            space = true;
            SceneManager.LoadScene(1);
        
        
        Debug.Log(currentPlanetsIndex);

        
        SetUI();
    }

    private void SetUI()
    {
       /* if (currentPlanetsIndex == 2)
            currentPlanetsIndex++;*/


        DistanceScript.planetName = planetsInfo.planetsNames[currentPlanetsIndex];
        DistanceScript.distanceToEnd = planetsInfo.planetsData.planetDistance;
        DistanceScript.updateStep = true;
    }

    private void Fade()
    {
        Instantiate(Resources.Load("-------UI Fade"));
    }

    public string GetNextName()
    {
        return planetsInfo.planetsNames[currentPlanetsIndex + 1];
    }


    public GameObject GetPlanetLandObstacle()
    {
        return planetsInfo.planetsData.SelectObstacleLand();
    }
    public GameObject GetLandStart()
    {
        return planetsInfo.planetsData.obstacleStart;
    }
    public GameObject[] GetAllLandSet()
    {
        return planetsInfo.planetsData.obstacleSet;
    }
    public Material GetCurrentMaterial()
    {
        return planetsInfo.planetsData.materialPlanet;
    }

    public void SetPlanetsData(PlanetsInfoData planetsData)
    {
        planetsInfo.planetsData = planetsData;
    }

    public Material GetMoonMaterial()
    {
        return planetsInfo.planetsData.spacePlanetLookMaterial;
    }

    public void SetEarthInfo()
    {
        if(currentPlanetsIndex == 2) {
            planetsInfo.planetsData = EarthMenager.Instance.GetInfoOfCurrentStage();
            MenagerAudio.Instance.PlayMusic(Sound.MusicList.CityEarthAtmosphereLoopV2);
            SetUI();
        }
    }

    public void UpdateEarthInfo()
    {
        planetsInfo.planetsData = EarthMenager.Instance.GetInfoOfCurrentStage();
    }


    //Scene Menager
    public void Restart()
    {
        SpawnSystem.RoundRestarted();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SetUI();
    }

    public void NextScene()
    {
        ChangeStage();
        SpawnSystem.speed += 1.5f;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(3);
    }

    [System.Serializable]
    public class PlanetsInfo
    {
        public string[] planetsNames;
        public PlanetsInfoData planetsData;

    }
}
