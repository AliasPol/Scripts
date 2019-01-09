using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour {

    public bool loadScene;
    public Transform monkeysSpawn;

    private void Awake() {

        if (loadScene) {

            LevelsMenager.Instance.StartLevel();
            string monkeyName = LevelsMenager.Instance.GetMonkey();
            if (monkeyName != null) {
                monkeysSpawn.gameObject.SetActive(true);
                Instantiate(Resources.Load("Monkeys/" + monkeyName), monkeysSpawn);
            }
        }
    }

    public void DestroyMonkeys() {
        Destroy(monkeysSpawn.gameObject);
    }
}
