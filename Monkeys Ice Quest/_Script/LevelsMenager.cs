using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMenager : MonoBehaviour {

    public int allLevels = 60;
    public int lastPlayedLevel;
    public int unlockedLevels;
    public string sceneName = "MainMenu";

    [HideInInspector] public bool showMonkey = true;

    [HideInInspector] public bool backFromGame = true;
    [HideInInspector] public int indexCurrentLevel;

    private static LevelsMenager instance;
    public static LevelsMenager Instance {
        get
        {
            if(instance == null) {
                instance = Instantiate(Resources.Load<LevelsMenager>("LevelsMenager")) as LevelsMenager;
                DontDestroyOnLoad(instance);
                instance.name = "LevelsMenager";
            }
            return instance;
        }
    }


    private void Awake() {
        lastPlayedLevel = PlayerPrefs.GetInt("LastLevelPlayed", 1);
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
    }

    public void PlayLevel(int levelIndex) {
        indexCurrentLevel = levelIndex;
    }

    public int StarsGetFromLevel(int indexOfLevel) {
        int stars = PlayerPrefs.GetInt("Level" + indexOfLevel, 0);
        return stars;
    }

    public void StarsSetForLevel(int indexOfLevel, int numberOfStar) {
        int i = PlayerPrefs.GetInt("Level" + indexOfLevel, 0);

        if (i < numberOfStar) {
            PlayerPrefs.SetInt("Level" + indexOfLevel, numberOfStar);
        }
    }

    public void UnlockedNewLevel() {
        if(indexCurrentLevel+1 > unlockedLevels && indexCurrentLevel < allLevels) {
            unlockedLevels = indexCurrentLevel+1;
            PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
        }
    }

    public void LastPlayedLevelChange(int levelIndex) {
        lastPlayedLevel = levelIndex;
        PlayerPrefs.SetInt("LastLevelPlayed", lastPlayedLevel);
    }

    public void StartLevel() {
        Debug.LogWarning("Levels/Level " + (indexCurrentLevel+1));
        Instantiate(Resources.Load("Levels/Level" + indexCurrentLevel));
    }

    public void SetArrayWithoutMistake() {
        int[] arrayMistake = PlayerPrefsX.GetIntArray("Mistake", 0, 60);
        arrayMistake[indexCurrentLevel - 1] = 1;
        PlayerPrefsX.SetIntArray("Mistake", arrayMistake);


        int check = 0;
        foreach(int i in arrayMistake) {
            if(i == 1) {
                check++;
            }
        }

        if(check == arrayMistake.Length) {
            AchivmentMenager.SetAchivment(GPGSls.achievement_mistakes_not_here);
            PlayerPrefs.SetInt("BreakingAnyBlock", 1);
        }
    }

    public void SetArrayPowersNotUse() {
        int[] arrayPowers = PlayerPrefsX.GetIntArray("Powers", 0, 60);
        arrayPowers[indexCurrentLevel - 1] = 1;
        PlayerPrefsX.SetIntArray("Powers", arrayPowers);

        int check = 0;
        foreach (int i in arrayPowers) {
            if (i == 1) {
                check++;
            }
        }

        if (check >= 5) {
            AchivmentMenager.SetAchivment(GPGSls.achievement_make_it_simple);
            PlayerPrefs.SetInt("Pass5Levels", 1);
        }
    }

    public void SetArrayMovesNotUse() {
        int[] arrayMoves = PlayerPrefsX.GetIntArray("Moves", 0, 60);
        arrayMoves[indexCurrentLevel - 1] = 1;
        PlayerPrefsX.SetIntArray("Moves", arrayMoves);

        int check = 0;
        foreach (int i in arrayMoves) {
            if (i == 1) {
                check++;
            }
        }

        if (check >= 10) {
            AchivmentMenager.SetAchivment(GPGSls.achievement_untouchable);
            PlayerPrefs.SetInt("Pass10Levels", 1);
        }
    }



    public string GetMonkey() {
        switch (indexCurrentLevel) {
            case 1:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel1";
                }
                return null;
            case 2:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel2";
                }
                return null;
           /* case 3:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel3";
                }
                return null;*/
            case 4:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel4";
                }
                return null;
            case 6:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel6";
                }
                return null;
            case 9:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel9";
                }
                return null;
            case 11:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel11";
                }
                return null;
            case 13:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel13";
                }
                return null;
            case 16:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel16";
                }
                return null;
            case 19:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel19";
                }
                return null;
            case 22:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel22";
                }
                return null;
            case 24:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel24";
                }
                return null;
            case 30:
                if (showMonkey) {
                    showMonkey = false;
                    return "MonkeyLevel30";
                }
                return null;
            default:
                return null;
            
        }
    }
}
