using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Planets info", menuName = "Planets/New Planets info")]
public class PlanetsInfoData : ScriptableObject {

    public string planetName;
    public float planetDistance;

    public Material materialPlanet;
    public Material spacePlanetLookMaterial;

    public GameObject[] obstacleLand;
    public GameObject obstacleStart;
    public GameObject[] obstacleSet;

    public GameObject SelectObstacleLand()
    {
        int index = Random.Range(0, obstacleLand.Length);
        return obstacleLand[index];
    }
}
