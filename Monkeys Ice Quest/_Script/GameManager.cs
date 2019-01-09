using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Power currentPower = Power.none;

    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<GameManager>();
            }
            return _Instance;
        }
    }


    public int powerMove;
    public int powerFreeze;
    public int powerSwap;
    public int powerCopy;
    public int powerHard;
    public int powerRotate;
    public int powerCollect;
    public int powerDoubled;

    public int moves;
    public int fruits;
    public int mistakes;

    private bool waitPanel;
    private bool winBlock;

    private void Awake() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("fruit");
        fruits = objects.Length;

        waitPanel = false;
        winBlock = false;

        TopBarMenu.Instance.MoveValue = moves + "";
        TopBarMenu.Instance.FruitValue = fruits + "";
        TopBarMenu.Instance.MistakesValue = mistakes + "";

        int powers = powerMove + powerFreeze + powerSwap + powerCopy + powerHard + powerRotate + powerCollect + powerDoubled;
        Score.Instance.SetScoreToGet(moves, mistakes, powers);
    }

    public void ChangeMistakesValue(int value) {
        mistakes += value;
        TopBarMenu.Instance.MistakesValue = mistakes + "";

        if (mistakes < 0 && !waitPanel)
            GameUI.Instance.EndScreen(false);
        else if (mistakes < 0) {
            WaitUntilEnd.Instance.DestroyedToMuchBlocks();
            winBlock = true;
        }
    }

    public void ChangeFruitsValue(int value) {
        fruits += value;
        TopBarMenu.Instance.FruitValue = fruits + "";

        if (fruits <= 0) {
            waitPanel = true;
            WaitUntilEnd.Instance.WaitPanel();
            /*
            SetYourScore();
            GameUI.Instance.EndScreen(true);
            */
        }
    }

    public void WaitPanelEnds() {
        SetYourScore();
        GameUI.Instance.EndScreen(!winBlock);
    }

    public void ChangeMovesValue(int value) {
        moves += value;
        TopBarMenu.Instance.MoveValue = moves + "";
        if (moves < 0)
            GameUI.Instance.EndScreen(false);
    }

    public void CollectedFruit() {
        ChangeFruitsValue(-1);
    }

    public void SetYourScore() {

        int powers = powerMove + powerFreeze + powerSwap + powerCopy + powerHard + powerRotate + powerCollect + powerDoubled;
        Score.Instance.YourScore(moves, mistakes, powers);
    }

    public int AllPowersLeft() {
        int powers = powerMove + powerFreeze + powerSwap + powerCopy + powerHard + powerRotate + powerCollect + powerDoubled;
        return powers;
    }

    public void ChangePowerValues(Power powerChange, int powerAmount) {

        switch (powerChange) {
            case Power.collect:
                powerCollect = powerAmount;
                break;
            case Power.copy:
                powerCopy = powerAmount;
                break;
            case Power.doubled:
                powerDoubled = powerAmount;
                break;
            case Power.freeze:
                powerFreeze = powerAmount;
                break;
            case Power.hard:
                powerHard = powerAmount;
                break;
            case Power.move:
                powerMove = powerAmount;
                break;
            case Power.rotate:
                powerRotate = powerAmount;
                break;
            case Power.swap:
                powerSwap = powerAmount;
                break;
            
        }
    }
}
