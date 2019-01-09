using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour {

    public static bool spawn = true;
    public static float speed = 10f;
    public static bool spawnFuel = true;

    public Vector3 spawnPosition;
    public GameObject[] allPrefabsSet;

    private float speedStep = 0.000004f;

    public bool land = false;
    private static float startSpeed;

    private void Awake()
    {
        startSpeed = speed;
        spawn = true;
        StartCoroutine(Spawn());
    }

    public void UpdateSet()
    {
        allPrefabsSet = RoundMenager.Instance.GetAllLandSet();
    }

    private IEnumerator Spawn()
    {
        if (land) {
            MenagerAudio.Instance.PlayMusic(Sound.MusicList.PlanetAtmosphere);
            RoundMenager.Instance.SetEarthInfo();


            allPrefabsSet = RoundMenager.Instance.GetAllLandSet();

            GameObject prefabStart = RoundMenager.Instance.GetLandStart();
            Instantiate(prefabStart, Vector3.zero, Quaternion.identity, transform);
          //  Instantiate(allPrefabsSet[0], new Vector3(0, 0, 180f), Quaternion.identity, transform);
          //  Instantiate(allPrefabsSet[0], new Vector3(0, 0, 360f), Quaternion.identity, transform);

        }
        else {
            MenagerAudio.Instance.PlayMusic(Sound.MusicList.OpenSpaceAtmosphere);
        }
        //540

        while (true) {

            if (spawn) {
                GameObject temp;
                int rnd = Random.Range(0, allPrefabsSet.Length);
                if (land) {
                    MoveForward children = GetComponentInChildren<MoveForward>();
                    Vector3 posV = children.gameObject.transform.position;
                    posV.z += spawnPosition.z;
                    temp = Instantiate(allPrefabsSet[rnd], posV, Quaternion.identity, transform) as GameObject;

                }
                else {
                    temp = Instantiate(allPrefabsSet[rnd], spawnPosition, Quaternion.identity, transform) as GameObject;
                }

                spawn = false;

                if (spawnFuel && !land) {
                    spawnFuel = false;
                    Vector3 pos = temp.transform.position;
                    Instantiate(RoundMenager.Instance.fuealPrefab, pos, Quaternion.identity, temp.transform);
                }
            }

            yield return new WaitForEndOfFrame();
            speed += speedStep;
        }
    }
    
    public static void RoundRestarted()
    {
        speed = startSpeed;
    }

    public void ChangeSceneBackground()
    {
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform) {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren) {
            Destroy(child.gameObject);
        }



        GameObject prefabStart = RoundMenager.Instance.GetLandStart();
        Instantiate(prefabStart, Vector3.zero, Quaternion.identity, transform);
        spawn = true;
    }
}
