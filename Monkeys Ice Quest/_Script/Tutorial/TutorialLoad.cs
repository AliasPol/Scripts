using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLoad : MonoBehaviour {

    private void Awake() {
        int index = LevelsMenager.Instance.indexCurrentLevel;

        LoadTutorial(index);
    }

    private void LoadTutorial(int number) {

        switch (number){
            case 1:
                Instantiate(Resources.Load("Levels/Level1Tutorial"));
                break;
            case 4:
                Instantiate(Resources.Load("Levels/Level4Tutorial"));
                break;
            case 6:
                Instantiate(Resources.Load("Levels/Level6Tutorial"));
                break;
            case 9:
                Instantiate(Resources.Load("Levels/Level9Tutorial"));
                break;
            case 11:
                Instantiate(Resources.Load("Levels/Level11Tutorial"));
                break;
            case 13:
                Instantiate(Resources.Load("Levels/Level13Tutorial"));
                break;
            case 16:
                Instantiate(Resources.Load("Levels/Level16Tutorial"));
                break;
            case 19:
                Instantiate(Resources.Load("Levels/Level19Tutorial"));
                break;
            case 22:
                Instantiate(Resources.Load("Levels/Level22Tutorial"));
                break;
            case 30:
                Instantiate(Resources.Load("Levels/Level30Tutorial"));
                break;
        }
    }
}
