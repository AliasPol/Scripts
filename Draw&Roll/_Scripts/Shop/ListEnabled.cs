using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEnabled : MonoBehaviour {
    public GameObject[] objects;

    private void Start() {
        StartCoroutine(LoadList());
    }

    private IEnumerator LoadList() {
        for(int i= 0; i< objects.Length; i++) {
            objects[i].SetActive(true);
            yield return new WaitForEndOfFrame();
        }
    }
}
